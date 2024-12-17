using Class;

namespace Class.MockData
{
    public static class MockGenerator
    {
        private static Random random = new Random();

        public static List<Role> GenerateMockRoles()
        {
            return new List<Role>
            {
                new Role { Id = 1, RoleName = "Admin"},
                new Role { Id = 2, RoleName = "Manager"},
                new Role { Id = 3, RoleName = "Staff"   },
                new Role { Id = 4, RoleName = "Customer"},
                new Role { Id = 5, RoleName = "Guest"   },
                new Role { Id = 6, RoleName = "Support" }
            };
        }

        public static List<Menu> GenerateMockMenus()
        {
            return new List<Menu>
            {
                new Menu { Id = 1, MenuName = "Dashboard" },
                new Menu { Id = 2, MenuName = "Settings" },
                new Menu { Id = 3, MenuName = "Profile" },
                new Menu { Id = 4, MenuName = "Reports" },
                new Menu { Id = 5, MenuName = "Notifications" },
                new Menu { Id = 6, MenuName = "Help" }
            };
        }

        public static List<Perm> GenerateMockPerms()
        {
            return new List<Perm>
            {
                new Perm { Id = 1, PermName = "No" },
                new Perm { Id = 2, PermName = "Read" },
                new Perm { Id = 3, PermName = "ReadWrite" }
            };
        }

        public static List<RoleMenuPerm> GenerateMockRoleMenuPerms(List<Role> roles, List<Menu> menus, List<Perm> perms)
        {
            var roleMenuPerms = new List<RoleMenuPerm>();

            foreach (var role in roles)
            {
                foreach (var menu in menus)
                {
                    var perm = perms[random.Next(perms.Count)];
                    roleMenuPerms.Add(new RoleMenuPerm
                    {
                        Role = role,
                        Menu = menu,
                        Perm = perm
                    });
                }
            }

            return roleMenuPerms;
        }
    }
}
