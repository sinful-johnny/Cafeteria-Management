﻿using api.Data;
using api.Dtos.Account;
using api.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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