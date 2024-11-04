using api.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CafeteriaDB;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<CANVA> Canva { get; set; }

        public DbSet<FOOD_TYPE> FoodType { get; set; }

        public DbSet<SHAPE_TYPE> ShapeType { get; set; }

        public DbSet<CAFETERIA_TABLE> CafeteriaTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FOOD_TABLE>()
            .HasKey(ct => new { ct.ID_FOOD, ct.ID_TABLE }); // Composite key for FOOD_TABLE

            modelBuilder.Entity<CANVA_ADMIN>()
                .HasKey(ca => new { ca.ID_CANVA, ca.ID_ADMIN }); // Composite key for CANVA_ADMIN

            base.OnModelCreating(modelBuilder);

            //Privileges
            //Stocks.FromSqlRaw
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },

                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles); //Add data into roles
        }
    }
}
