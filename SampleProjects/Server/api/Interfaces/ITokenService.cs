using api.Models;
using CafeteriaDB;

namespace api.Interfaces
{
    public interface ITokenService
    {
        public Task<string> CreateToken(ADMIN admin);
    }
}
