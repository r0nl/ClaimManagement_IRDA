using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRDA_DAL.Model
{
    public class paymentOfClaim
    {
        [Key]
        [Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string reportId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Payment must be non-negative")]
        public int payment {  get; set; }

        [Required]
        [StringLength(3)]
        public string month { get; set; }

        [Required]
        public int year { get; set; }
    }
}
