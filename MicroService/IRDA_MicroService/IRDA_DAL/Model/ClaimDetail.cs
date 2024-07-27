using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRDA_DAL.Model
{
    public class ClaimDetail
    {
        [Length(10, 10, ErrorMessage = "The length of the ClaimId must be 10 characters.")]
        public string? ClaimId { get; set; }
        public string PolicyNo { get; set; }
        public Policy Policy { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "EstimatedLoss must be non-negative")]
        public int EstimatedLoss { get; set; }

        [DateLessThanCurrent(ErrorMessage = "The DateOfAccident must be less than current date.")]
        public DateOnly DateOfAccident { get; set; }
        public string ClaimStatus { get; set; }

        public int? SurveyorID { get; set; }
        public Surveyor? Surveyor { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "EstimatedLoss must be non-negative")]
        public int AmtApprovedBySurveyor { get; set; }
        public bool InsuranceCompanyApproval { get; set; }
        public bool WithdrawClaim { get; set; }
        public int? SurveyorFees { get; set; }
    }
}
