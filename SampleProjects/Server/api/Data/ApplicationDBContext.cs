using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using cafeteriaDBLocalHost;
using Microsoft.Extensions.Configuration;
using api.Dtos.Account;
using api.Dtos.USER;

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

        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<ADMIN> Admin {  get; set; }

        public DbSet<CANVA> Canva { get; set; }

        public DbSet<FOOD_TYPE> FoodType { get; set; }

        public DbSet<SHAPE_TYPE> ShapeType { get; set; }

        public DbSet<CAFETERIA_TABLE> CafeteriaTable { get; set; }

        public DbSet<V_ADMIN_TABLEInCANVA> V_TableInCanva { get; set; }

        public DbSet<V_ADMIN_FOODsOnTABLE> V_FoodsOnTable { get; set; }

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

            modelBuilder.Entity<UserRole>()
                .HasNoKey();

            modelBuilder.Entity<V_ADMIN_TABLEInCANVA>()
                .HasKey(v => new { v.ID_TABLE }); // Composite key for V_ADMIN_TABLEInCANVA

            modelBuilder.Entity<V_ADMIN_FOODsOnTABLE>()
                .HasKey(v => new { v.ID_FOOD, v.ID_TABLE }); // Composite key for V_FoodsOnTable

            modelBuilder.Entity<FOOD_TABLE>()
                .HasKey(ct => new { ct.ID_FOOD, ct.ID_TABLE }); // Composite key for FOOD_TABLE

            modelBuilder.Entity<CANVA_ADMIN>()
                .HasKey(ca => new { ca.ID_CANVA, ca.ID_ADMIN }); // Composite key for CANVA_ADMIN

            modelBuilder.Entity<ADMIN>()
                .HasKey(a => new { a.ID_ADMIN }); // Composite key for ADMIN

            modelBuilder.Entity<CAFETERIA_TABLE>()
                .HasKey(ct => new { ct.ID_TABLE }); // Composite key for CAFETERIA_TABLE

            modelBuilder.Entity<CANVA>()
                .HasKey(c => new { c.ID_CANVA }); // Composite key for CANVA

            modelBuilder.Entity<FOOD_TYPE>()
                .HasKey(f => new { f.ID_FOOD }); // Composite key for FOOD_TYPE

            modelBuilder.Entity<SHAPE_TYPE>()
                .HasKey(s => new { s.ID_SHAPE }); // Composite key for SHAPE_TYPE

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

            //List<IdentityRole> roles = new List<IdentityRole>
            //{
            //    new IdentityRole
            //    {
            //        Name = "Admin",
            //        NormalizedName = "ADMIN"
            //    },

            //    new IdentityRole
            //    {
            //        Name = "User",
            //        NormalizedName = "USER"
            //    }
            //};

            //modelBuilder.Entity<IdentityRole>().HasData(roles); //Add data into roles
        }
    }
}
