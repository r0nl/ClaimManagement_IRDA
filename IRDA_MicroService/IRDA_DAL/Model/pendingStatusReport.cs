using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRDA_DAL.Model
{
    public class pendingStatusReport
    {
        //[Key]
        [Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string reportId { get; set; }

        [Required]
        public string stage {  get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Count must be non-negative")]
        public int count { get; set; }

        [Required]
        [StringLength(3)]
        public string month { get; set; }

        [Required]
        public int year { get; set; }
    }
}
