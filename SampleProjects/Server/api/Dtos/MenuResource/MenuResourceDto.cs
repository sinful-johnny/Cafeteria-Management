

using api.Dtos.MenuResource;

namespace api.Dtos.MenuResource
{
    public class MenuResourceDto
    {
        public int menuID { get; set; }
        public string MenuName { get; set; }
        public List<RolePermissionDto> OwnerRoles { get; set; }= new List<RolePermissionDto>();
        public List<MenuResourceDto>? children { get; set; } = new List<MenuResourceDto>();
    }
}
