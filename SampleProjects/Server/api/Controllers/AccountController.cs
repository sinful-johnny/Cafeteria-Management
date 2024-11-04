using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using api.Repository;
using CafeteriaDB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IAdminRepository _adminRepo;

        public AccountController(IAdminRepository adminRepo, ITokenService tokenService)
        {
            _adminRepo = adminRepo;
            _tokenService = tokenService;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var admin = new ADMIN
                {
                    EMAIL = registerDto.EmailAddress
                };

                if (registerDto.Password == null)
                {
                    return BadRequest(ModelState);
                }


                string result = await _adminRepo.RegisterAdminAsync(registerDto);
                if (result.Contains("User registered successfully"))
                {
                    return Ok
                            (
                                new NewUserDto
                                {
                                    Email = admin.EMAIL,
                                    Token = _tokenService.CreateToken(admin)
                                }
                            );
                }
                else
                {
                    return BadRequest(new { Message = result });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
