using IdentityCafeteriaModel;

namespace api.Interfaces
{
    public interface IMenuResource_Repository
    {
        public Task<(List<V_Menu>, List<V_Role_Menu>, List<V_Permission_RoleMenu>)> getAllMenuResource();
    }
}
