using api.Dtos.USER;
using cafeteriaDBLocalHost;

namespace api.Interfaces
{
    public interface IUserService
    {
        Task<ADMIN?> GetUserByUsernameAsync(string userEmail);
        Task<List<string>> GetUserRolesAsync(ADMIN user);
    }
}
