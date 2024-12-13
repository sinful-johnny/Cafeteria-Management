using api.Dtos.FOOD;
using api.Dtos.FOOD_TABLE;
using api.Dtos.MenuResource;
using IdentityCafeteriaModel;

namespace api.Mappers
{
    public static class MenuResourceMapper
    {
        public static MenuResourceDto ToMenuResourceDto(this V_Menu Menu, List<MenuResourceDto> MenuChildrens, List<RolePermissionDto> rolePermissions)
        {
            return new MenuResourceDto
            {
                menuID = Menu.menuID,
                MenuName = Menu.menuName,
                OwnerRoles = rolePermissions,
                children = MenuChildrens
            };
        }

        public static RolePermissionDto ToRolePermissionDto(this V_Role_Menu roleMenu, List<PermissionDto> permissions)
        {
            return new RolePermissionDto
            {
                menuID = roleMenu.menuID,
                rolemenuID = roleMenu.rolemenuID,
                RoleName = roleMenu.roleName,
                PermissionType = permissions
            };
        }

        public static PermissionDto ToPermissionDto(this V_Permission_RoleMenu permissions)
        {
            return new PermissionDto
            {
                rolemenuID = permissions.rolemenuID,
                PermissionName = permissions.permissionName
            };
        }
    }
}
