using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Restoran_Loyiha.Models
{
    public class AppDbContext: IdentityDbContext
    {
       

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<AsosiySahifa> AsosiySahifalar { get; set; }
        public DbSet<BizHaqimizda> BizHaqimizda { get; set; }
        public DbSet<Oshpaz> Oshpazlar { get; set; }
        public DbSet<Taom> Taomlar { get; set; }
        public DbSet<Stol> Stollar { get; set; }
        public DbSet<Buyurtma> Buyurtmalar { get; set; }
        public DbSet<BuyurtmaElementlari> BuyurtmaElementlari { get; set; }
        public DbSet<Bron> Bronlar { get; set; }
        public DbSet<Aloqa> Aloqalar { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Pul va summa uchun decimal(18,2)
            modelBuilder.Entity<Buyurtma>()
                .Property(b => b.Summa)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<BuyurtmaElementlari>()
                .Property(e => e.UmumiyNarx)
                .HasColumnType("decimal(18,2)");
        }
    }

}
