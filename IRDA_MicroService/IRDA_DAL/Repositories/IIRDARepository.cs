using IRDA_DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRDA_DAL.Repositories
{
    public interface IIRDARepository
    {
        public List<pendingStatusReport>? GetClaimStatusByMonthAndYear(string month, int year) { return null; }

        public List<paymentOfClaim>? GetPaymentStatusByMonthAndYear(string month, int year) { return null; }

        public List<ClaimDetail>? GetClaimDetails(string month, int year) { return null; }

        public bool AddClaimReport(pendingStatusReport PSR) { return false; }

        public bool AddPaymentReport(paymentOfClaim POC) { return false; }

        public string[] GetReportIdsClaims() { return []; }

        public string[] GetReportIdsPayments() { return []; }

        public bool DeleteClaimRecord(string recordId) { return false; }

        public bool DeletePaymentRecord(string recordId) { return false; }
    }
}
