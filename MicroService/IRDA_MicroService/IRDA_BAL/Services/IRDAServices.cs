using IRDA_BAL.DTOs;
using IRDA_DAL.Model;
using IRDA_DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IRDA_BAL.Services
{
    public static class HelperFunctions
    {
        public static Dictionary<string, int> Month = new Dictionary<string, int>()
        {
            { "Jan", 1}, {"Feb", 2}, {"Mar", 3},
            { "Apr", 4}, {"May", 5}, {"Jun", 6},
            { "Jul", 7}, {"Aug", 8}, {"Sep", 9},
            { "Oct", 10}, {"Nov", 11}, {"Dec", 12},
        };

        public static string GenerateReportId(List<ClaimDetail> cd, string m, int y)
        {
            string code = cd.All(c => c.ClaimStatus == "Closed") ? "CS" : "PS";
            string y_lastdigits = $"{y}".Substring(2);
            return code + m + y_lastdigits;
        }

        public static Dictionary<string, List<ClaimDetail>> GroupByStages(List<ClaimDetail> cd)
        {
            return cd.GroupBy(c => c.ClaimStatus switch
            {
                "Open" when c.SurveyorID == null => "New Claims",
                "Open" when c.SurveyorID != null => "Pending Claims",
                "Closed" => "Finalized Claims",
                _ => "Undefined"
            }).ToDictionary(g => g.Key, g => g.ToList());
        }

        public static Dictionary<string, int> GetCountByStages(Dictionary<string, List<ClaimDetail>> cd)
        {
            return cd.ToDictionary(g => g.Key, g => g.Value.Count);
        }
    }


    public class IRDAServices : IIRDAServices
    {
        private readonly IIRDARepository _repo;
        public IRDAServices(IIRDARepository repo)
        {
            _repo = repo;
        }
        public List<ClaimReportDTO>? ClaimStatusByMonthAndYear(string month, int year)
        {
            var claimReport = _repo.GetClaimStatusByMonthAndYear(month, year);
            return claimReport
                .GroupBy(r => r.stage)
                .Select(g => new ClaimReportDTO { Status = g.Key, TotalCount = g.Sum(c => c.count) })
                .ToList();
        }

        public List<ClaimPaymentReportDTO>? PaymentStatusByMonthAndYear(string month, int year) 
        {
            var paymentReport = _repo.GetPaymentStatusByMonthAndYear(month, year);

            return paymentReport
                .GroupBy(r => r.month)
                .Select(g => new ClaimPaymentReportDTO { Month = g.Key, TotalCost = g.Sum(r => r.payment) })
                .ToList();
        }

        public bool PaymentStatusToDatabase(string month, int year) 
        {

            List<ClaimDetail> claimDetails = _repo.GetClaimDetails(month, year);

            string reportId = HelperFunctions.GenerateReportId(claimDetails, month, year);

            string[] reportIds = _repo.GetReportIdsPayments();
            if (reportIds.Contains(reportId))
            {
                // throw new Exception($"Database already updated for {month}, {year}");
                bool validatingdel = _repo.DeletePaymentRecord(reportId);
            }

            int payment = claimDetails.Sum(c => c.AmtApprovedBySurveyor);

            paymentOfClaim pc = new paymentOfClaim
            {
                reportId = reportId,
                payment = payment,
                month = month,
                year = year
            };

            bool check = _repo.AddPaymentReport(pc);
            return check;

            
        }

        public bool ClaimStatusToDatabase(string month, int year) 
        {
            List<ClaimDetail> claimDetails = _repo.GetClaimDetails(month, year);

            if (claimDetails.Count < 1)
            {
                throw new Exception("No Claim Found!");
            }


            string reportId = HelperFunctions.GenerateReportId(claimDetails, month, year);

            string[] reportIds = _repo.GetReportIdsClaims();
            if (reportIds.Contains(reportId))
            {
                // throw new Exception($"Database already updated for {month}, {year}");
                bool validatingDel = _repo.DeleteClaimRecord(reportId);
            }

            Dictionary<string, List<ClaimDetail>> claimDict = HelperFunctions.GroupByStages(claimDetails);

            Dictionary<string, int> count = HelperFunctions.GetCountByStages(claimDict);

            foreach (var key in claimDict.Keys)
            {
                pendingStatusReport p = new pendingStatusReport
                {
                    reportId = reportId,
                    count = count[key],
                    month = month,
                    stage = key,
                    year = year
                };
                bool check = _repo.AddClaimReport(p);

                if (!check) return false;

            }
            return true;
        }
    }
}
