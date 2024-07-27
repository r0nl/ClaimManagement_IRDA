using IRDA_BAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRDA_BAL.Services
{
    public interface IIRDAServices
    {
        public List<ClaimReportDTO>? ClaimStatusByMonthAndYear(string month, int year) { return null; }

        public List<ClaimPaymentReportDTO>? PaymentStatusByMonthAndYear(string month, int year) { return null; }

        public bool PaymentStatusToDatabase(string month, int year);

        public bool ClaimStatusToDatabase(string month, int year);
    }

}
