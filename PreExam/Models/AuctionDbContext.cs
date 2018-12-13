using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreExam.Models
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
        {

        }

        public DbSet<AuctionItem> AuctionItems { get; set; }

        public DbSet<Bid> Bids { get; set; }



    }
}
