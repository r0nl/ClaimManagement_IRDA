using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRDA_DAL.Model
{
    public class Policy
    {

        [Length(7, 7, ErrorMessage = "PolicyNo should be of 7 characters")]
        public string PolicyNo { get; set; }

        [MinLength(5, ErrorMessage = "InsuredFirstName should have atleast 5 characters")]
        public string InsuredFirstName { get; set; }

        [MinLength(5, ErrorMessage = "InsuredLastName should have atleast 5 characters")]
        public string InsuredLastName { get; set; }

        [DateGreaterEqualThanCurrent(ErrorMessage = "The DateOfInsurance must not be less than current date.")]
        public DateOnly DateOfInsurance { get; set; }
        public string? EmailId { get; set; }
        public string? VehicleNo { get; set; }
        public bool status { get; set; }

        public ICollection<ClaimDetail> claimDetails { get; set; }
    }

}
