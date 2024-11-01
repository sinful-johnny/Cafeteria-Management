using Cafeteria_DB;
using EFTut.ModelAppuser;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EFTut.Data
{
    public class ApplicationDBContext : DbContext//IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {


        }
        //public DbSet<ADMIN> Admin { get; set; }

        public DbSet<CANVA> Canva { get; set; }

        public DbSet<FOOD_TYPE> FoodType { get; set; }

        public DbSet<SHAPE_TYPE> ShapeType { get; set; }

        public DbSet<CAFETERIA_TABLE> CafeteriaTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FOOD_TABLE>()
            .HasKey(ct => new { ct.ID_FOOD, ct.ID_TABLE}); // Composite key for FOOD_TABLE

            modelBuilder.Entity<CANVA_ADMIN>()
                .HasKey(ca => new { ca.ID_CANVA, ca.ID_ADMIN }); // Composite key for CANVA_ADMIN

            base.OnModelCreating(modelBuilder);
        }


        //Privileges
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    List<IdentityRole> roles = new List<IdentityRole>
        //    {
        //        new IdentityRole
        //        {
        //            Name = "Admin",
        //            NormalizedName = "ADMIN"
        //        },

        //        new IdentityRole
        //        {
        //            Name = "User",
        //            NormalizedName = "USER"
        //        }
        //    };

        //    builder.Entity<IdentityRole>().HasData(roles); //Add data into roles
        //}
    }
}
