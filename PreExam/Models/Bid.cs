using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreExam.Models
{
    public class Bid
    {
        [Required]
        [Key]
        //[Range(9999, 11000)]
        public int ItemNumber { get; set; }

        [Required]
        [Display(Description = "Price must be higher than previous bid or rating price")]
        public int Price { get; set; }

        [MaxLength(50)]
        [Required]
        public string CustomName { get; set; }

        [Required]
        [MaxLength(12)]
        public string CustomPhone { get; set; }
    }
}