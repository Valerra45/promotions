using Domain.Promotions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options) { }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<Partner> Partner { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Goods> Goods { get; set; }

        public DbSet<Promotion> Promotions { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<SendPromotion> SendPromotions { get; set; }
    }
}
