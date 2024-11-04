using api.Dtos.Account;

namespace api.Interfaces
{
    public interface IAdminRepository
    {
        public Task<string> RegisterAdminAsync(RegisterDto registerDto);
    }
}
