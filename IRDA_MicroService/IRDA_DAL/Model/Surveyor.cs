using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRDA_DAL.Model
{
    public class Surveyor
    {
        public int SurveyorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The Estimated Limit must be a positive number")]
        public int EstimateLimit { get; set; }
        public int TimesAllocated { get; set; }

        public ICollection<ClaimDetail> claimDetails { get; set; }
    }
}
