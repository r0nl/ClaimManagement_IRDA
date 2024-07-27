using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRDA_DAL.Model
{
    ///<summary>
    ///It checks if the DateOfInsurance is greater than or equal to the current date. It shows validation error if it is less than the current date.
    ///</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class DateGreaterEqualThanCurrentAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var policy = (Policy)validationContext.ObjectInstance;
            if (policy.DateOfInsurance >= DateOnly.FromDateTime(DateTime.Today)) return ValidationResult.Success;
            return new ValidationResult(this.ErrorMessage);
        }
    }
}
