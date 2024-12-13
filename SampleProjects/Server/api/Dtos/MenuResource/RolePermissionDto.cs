namespace api.Dtos.MenuResource
{
    public class RolePermissionDto
    {
        public string RoleName { get; set; }
        public int menuID { get; set; }
        public int rolemenuID { get; set; }
        public List<PermissionDto> PermissionType { get; set; } = new List<PermissionDto>();
    }
}
