using Class.Auth;
using System.Text.Json;

namespace Class.Converter
{
    public class MenuResourceDto
    {
        public int menuID { get; set; }
        public string menuName { get; set; }
        public List<RolePermissionDto> ownerRoles { get; set; } = new List<RolePermissionDto>();
        public List<MenuResourceDto>? children { get; set; } = new List<MenuResourceDto>();
    }

    public class RolePermissionDto
    {
        public string roleName { get; set; }
        public int menuID { get; set; }
        public int rolemenuID { get; set; }
        public List<PermissionDto> permissionType { get; set; } = new List<PermissionDto>();
    }
    public class PermissionDto
    {
        public int rolemenuID { get; set; }
        public string permissionName { get; set; }
    }
    public static class JsonToMenuResourceConverter
    {
        public static MenuResource? Convert(string json)
        {
            var menuResourceDto = JsonSerializer.Deserialize<List<MenuResourceDto>>(json);
            if(menuResourceDto != null)
            {
                var list = menuResourceDto.Select(m => ConvertToMenuResource(m)).ToList();
                MenuResource menuResource = list[0];
                return menuResource;
            }
            else
            {
                return null;
            }
        }

        public static MenuResource ConvertToMenuResource(MenuResourceDto dto)
        {
            return new MenuResource
            {
                MenuName = dto.menuName,
                OwnerRoles = dto.ownerRoles.Select(roleDto => new RolePermission
                {
                    RoleName = roleDto.roleName,
                    PermissionType = roleDto.permissionType.Select(permission => permission.permissionName).ToList()
                }).ToList(),
                Children = dto.children?.Select(ConvertToMenuResource).ToList()
            };
        }

    }
}
