using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CafeteriaDB;
using Microsoft.Extensions.Configuration;
using api.Dtos.Account;
using api.Dtos.USER;
using api.Identity;
using IdentityCafeteriaModel;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        //private readonly string _connectionString;
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions)
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


        //Entity for Identity
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuPermission> MenuPermissions { get; set; }
        public DbSet<Identity.Permission> Permissions { get; set; }
        public DbSet<ApplicationRoleMenu> RoleMenus { get; set; }

        //View for Identity
        public DbSet<V_Menu> VMenus { get; set; }
        public DbSet<V_Permission_RoleMenu> VPermission_Roles { get; set; }
        public DbSet<V_Role_Menu> VRole_Menus { get; set; }

        //Entity for Db
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

            //Constraints for Identity
            modelBuilder.Entity<MenuItem>(item =>
            {
                item.ToTable("AspNetMenu");
                item.HasMany(y => y.Children)
                    .WithOne(r => r.ParentItem)
                    .HasForeignKey(u => u.ParentId);

                item.HasMany(t => t.RoleMenus)
                    .WithOne(u => u.MenuItem)
                    .HasForeignKey(r => r.MenuId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<ApplicationRoleMenu>(roleMenu =>
            {
                roleMenu.ToTable("AspNetRoleMenu");

                roleMenu.HasOne(o => o.Role)
                    .WithMany(u => u.RoleMenus)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.NoAction);

                roleMenu.HasOne(o => o.MenuItem)
                    .WithMany(u => u.RoleMenus)
                    .HasForeignKey(e => e.MenuId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<MenuPermission>(mp =>
            {
                mp.ToTable("MenuPermission");

                mp.HasKey(l => new { l.RoleMenuId, l.PermissionId });

                mp.HasOne(o => o.Permission)
                    .WithMany(i => i.MenuPermissions)
                    .IsRequired();

                mp.HasOne(o => o.RoleMenu)
                    .WithMany(i => i.Permissions)
                    .IsRequired();
            });

            modelBuilder.Entity<Identity.Permission>(mp =>
            {
                mp.ToTable("Permission");

                mp.HasKey(l => l.Id);

                mp.HasMany(o => o.MenuPermissions)
                    .WithOne(i => i.Permission)
                    .HasForeignKey(y => y.PermissionId);
            });

            modelBuilder.Entity<V_Menu>()
                .HasKey(v => new { v.menuID }); // Composite key for V_Menu

            modelBuilder.Entity<V_Permission_RoleMenu>()
                .HasKey(v => new { v.rolemenuID }); // Composite key for V_Permission_RoleMenu

            modelBuilder.Entity<V_Role_Menu>()
                .HasKey(v => new { v.rolemenuID }); // Composite key for V_Role_Menu

            //Constraints for CafeteriaDB

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
