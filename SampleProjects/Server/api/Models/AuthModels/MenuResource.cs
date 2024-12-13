

namespace api.Models.AuthModels
{
    public class MenuResourceDto
    {
        public required string MenuName { get; set; }
        public required List<RolePermission> OwnerRoles { get; set; }
        public List<MenuResourceDto>? children { get; set; }
    }

    public class RolePermission
    {
        public required string RoleName { get; set; }
        public required List<string> PermissionType { get; set; }
    }
}
