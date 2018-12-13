using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreExam.Models
{
    public class AuctionItem
    {
        [Key]
        //[Range(9999, 11000)]
        public int ItemNumber { get; set; }

        [Required]
        [MaxLength(150)]
        public string ItemDescription { get; set; }

        [Required]
        public int RatingPrice { get; set; }

        public int? BidPrice { get; set; }

        public string BidCustomName { get; set; }

        public string BidCustomePhone { get; set; }

        public DateTime BidTimeStamp { get; set; }
    }
}