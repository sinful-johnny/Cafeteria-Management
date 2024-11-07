using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Cafeteria_DB;
using Microsoft.Extensions.Configuration;
using api.Dtos.Account;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        //private readonly string _connectionString;
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        //private string BuildConnectionString(string baseConnectionString, string userId, string password)
        //{
        //    return baseConnectionString
        //        .Replace("{UserId}", userId)
        //        .Replace("{Password}", password);
        //}

        // New constructor for SQL Authentication
        //public ApplicationDBContext(LoginDto loginDto, IConfiguration configuration)
        //{
        //    // Build the connection string with SQL Authentication credentials
        //    var baseConnectionString = configuration.GetConnectionString("LoginConnection");
        //    _connectionString = BuildConnectionString(baseConnectionString, loginDto.EmailAddress, loginDto.Password);
        //}

        public DbSet<CANVA> Canva { get; set; }

        public DbSet<FOOD_TYPE> FoodType { get; set; }

        public DbSet<SHAPE_TYPE> ShapeType { get; set; }

        public DbSet<CAFETERIA_TABLE> CafeteriaTable { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(_connectionString))
        //    {
        //        optionsBuilder.UseSqlServer(_connectionString);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FOOD_TABLE>()
            .HasKey(ct => new { ct.ID_FOOD, ct.ID_TABLE }); // Composite key for FOOD_TABLE

            modelBuilder.Entity<CANVA_ADMIN>()
                .HasKey(ca => new { ca.ID_CANVA, ca.ID_ADMIN }); // Composite key for CANVA_ADMIN

            modelBuilder.Entity<FOOD_TABLE>()
                .HasOne(u => u.FOOD_TYPE)
                .WithMany(u => u.FOOD_TABLE)
                .HasForeignKey(p => p.ID_FOOD);

            modelBuilder.Entity<FOOD_TABLE>()
                .HasOne(u => u.CAFETERIA_TABLE)
                .WithMany(u => u.FOOD_TABLE)
                .HasForeignKey(p => p.ID_TABLE);

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
