using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRDA_DAL.Model
{
    ///<summary>
    ///It checks if the DateOfAccident is less than the current date. It shows validation error if it is greater or equal to the current date.
    ///</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class DateLessThanCurrentAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var claimDetail = (ClaimDetail)validationContext.ObjectInstance;
            if (claimDetail.DateOfAccident < DateOnly.FromDateTime(DateTime.Today)) return ValidationResult.Success;
            return new ValidationResult(this.ErrorMessage);
        }
    }
}
