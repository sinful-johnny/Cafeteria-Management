using api.Data;
using api.Dtos.USER;
using api.Interfaces;
using CafeteriaDB;
using Microsoft.EntityFrameworkCore;

namespace api.Service
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;

        public UserService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ADMIN?> GetUserByUsernameAsync(string userEmail)
        {
            return await _context.Admin
                .FromSqlRaw("EXEC DBO.Take_GetUserByEmail @userEmail={0}", userEmail)
                .FirstOrDefaultAsync();
        }

        public async Task<List<string>> GetUserRolesAsync(ADMIN user)
        {
            var userRoles = await _context.userRoles
                .FromSqlRaw("EXEC DBO.Take_UserRoles @userEmail={0}", user.EMAIL)
                //.Select(ur => ur.ROLE_NAME)
                .ToListAsync();

            List<string> roles = new List<string>();

            foreach (var userRole in userRoles)
            {
                roles.Add(userRole.ROLE_NAME);
            }

            return roles.Any() ? roles : new List<string> { "NoRoles" };
        }
    }
}
