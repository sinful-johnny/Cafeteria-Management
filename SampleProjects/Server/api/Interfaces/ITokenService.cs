using api.Models;
using cafeteriaDBLocalHost;

namespace api.Interfaces
{
    public interface ITokenService
    {
        public Task<string> CreateToken(ADMIN admin);
    }
}
