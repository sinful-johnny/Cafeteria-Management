using api.Models;
using CafeteriaDB;

namespace api.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(ADMIN admin);
    }
}
