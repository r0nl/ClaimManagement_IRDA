using IRDA_DAL.DbContexts;
using IRDA_DAL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRDA_DAL.Repositories
{
    public class IRDARepository : IIRDARepository
    {
        private readonly IRDAContext _context;

        public IRDARepository(IRDAContext context)
        {
            _context = context;
        }

        public static Dictionary<string, int> Month = new Dictionary<string, int>()
        {
        { "Jan", 1}, {"Feb", 2}, {"Mar", 3},
        { "Apr", 4}, {"May", 5}, {"Jun", 6},
        { "Jul", 7}, {"Aug", 8}, {"Sep", 9},
        { "Oct", 10}, {"Nov", 11}, {"Dec", 12},
        };

        public List<pendingStatusReport>? GetClaimStatusByMonthAndYear(string month, int year)
        {
            var releventData = _context.pendingStatusReports
                .Where(r => r.month == month && r.year == year)
                .ToList();

            return releventData;
        }

        public List<paymentOfClaim>? GetPaymentStatusByMonthAndYear(string month, int year)
        {
            var releventData = _context.paymentOfClaims
                .Where(r => r.month == month && r.year == year)
                .ToList();

            return releventData;
        }

        public List<ClaimDetail>? GetClaimDetails(string month, int year)
        {
            return _context.ClaimDetails
                .Where(c => c.DateOfAccident.Year == year && c.DateOfAccident.Month == Month[month])
                .ToList();
        }

        public bool AddClaimReport(pendingStatusReport PSR)
        {
            _context.pendingStatusReports.Add(PSR);
            _context.SaveChanges();
            return true;
        }

        public bool AddPaymentReport(paymentOfClaim POC) 
        {
            _context.paymentOfClaims.Add(POC);
            _context.SaveChanges();
            return true; 
        }

        public string[] GetReportIdsClaims() 
        { 
            return _context.pendingStatusReports.Select(r => r.reportId).ToArray();
        }

        public string[] GetReportIdsPayments()
        {
            return _context.paymentOfClaims.Select(r => r.reportId).ToArray();
        }

        public bool DeleteClaimRecord(string recordId) 
        {
            pendingStatusReport[] pr = _context.pendingStatusReports.Where(r => r.reportId == recordId).ToArray();
            foreach (pendingStatusReport p in pr)
            {
                _context.pendingStatusReports.Remove(p);
                _context.SaveChanges();
            }

            return true; 
        }

        public bool DeletePaymentRecord(string recordId) 
        {

            paymentOfClaim pc = _context.paymentOfClaims.Where(r => r.reportId == recordId).First();
            _context.paymentOfClaims.Remove(pc);
            _context.SaveChanges();
            return true;
        }
    }
}
