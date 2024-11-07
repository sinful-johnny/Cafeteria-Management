using api.Models;
using cafeteriaDBLocalHost;

namespace api.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(ADMIN admin);
    }
}
