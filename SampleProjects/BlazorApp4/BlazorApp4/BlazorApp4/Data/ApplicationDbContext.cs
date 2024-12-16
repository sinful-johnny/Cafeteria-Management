using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp4.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public required DbSet<MenuItem> MenuItems { get; set; }
        public required DbSet<MenuPermission> MenuPermissions { get; set; }
        public required DbSet<Permission> Permissions { get; set; }
        public required DbSet<ApplicationRoleMenu> RoleMenus { get; set; }

        public required DbSet<APIPermission> APIPermissions { get; set; }
        public required DbSet<ApplicationAPI> ApplicationAPIs { get; set; }
        public required DbSet<ApplicationRoleAPI> ApplicationRoleAPIs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Additional tables.

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

            modelBuilder.Entity<Permission>(mp =>
            {
                mp.ToTable("Permission");

                mp.HasKey(l => l.Id);

                mp.HasMany(o => o.MenuPermissions)
                    .WithOne(i => i.Permission)
                    .HasForeignKey(y => y.PermissionId);
            });

            modelBuilder.Entity<ApplicationAPI>(item =>
            {
                item.ToTable("AspNetAPI");

                item.HasMany(t => t.RoleApis)
                    .WithOne(u => u.API)
                    .HasForeignKey(r => r.ApiId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<APIPermission>(mp =>
            {
                mp.ToTable("ApiPermission");

                mp.HasKey(l => new { l.RoleApiId, l.PermissionId });
            });
        }
    }
}
