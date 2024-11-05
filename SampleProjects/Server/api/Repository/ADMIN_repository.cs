using api.Data;
using api.Dtos.Account;
using api.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace api.Repository
{
    public class ADMIN_repository: IAdminRepository
    {
        private readonly ApplicationDBContext _context;

        public ADMIN_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<string> LoginAdminAsync(LoginDto loginDto)
        {
            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255) 
            { 
                Direction = ParameterDirection.Output 
            }; 
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_ADMIN_LOGIN @Email, @Password, @ResponseMessage OUTPUT",
                new SqlParameter("@Email", loginDto.EmailAddress), 
                new SqlParameter("@Password", loginDto.Password), responseMessage);

            //using var sqlAuthContext = new ApplicationDBContext(loginDto, _configuration);

            return responseMessage.Value.ToString();
        }

        public async Task<string> RegisterAdminAsync(RegisterDto registerDto)
        {
            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_ADMIN_REGISTER @Email, @Password, @ResponseMessage OUTPUT",
                new SqlParameter("@Email", registerDto.EmailAddress),
                new SqlParameter("@Password", registerDto.Password),
                responseMessage
            );

            return responseMessage.Value.ToString();
        }
    }

}
