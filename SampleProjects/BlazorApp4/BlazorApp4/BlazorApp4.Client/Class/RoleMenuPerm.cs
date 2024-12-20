using Class;

namespace Class
{
    public class RoleMenuPerm
    {
        public required Role Role { get; set; }
        public required Menu Menu { get; set; }
        public List<Perm>? Perms { get; set; }
    }
}
