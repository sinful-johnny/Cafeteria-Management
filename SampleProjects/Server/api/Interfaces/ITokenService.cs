using api.Models;
using Cafeteria_DB;

namespace api.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(ADMIN admin);
    }
}
