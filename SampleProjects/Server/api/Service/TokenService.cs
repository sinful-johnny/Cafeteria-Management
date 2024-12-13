using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Interfaces;
using api.Models;
using cafeteriaDBLocalHost;
using Microsoft.IdentityModel.Tokens;


namespace api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly IUserService _userService;
        public TokenService(IConfiguration config, IUserService userService) 
        {
            this._config = config;
            this._key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            this._userService = userService;
        }
        public async Task<string> CreateToken(ADMIN admin)
        {

            var roles = await _userService.GetUserRolesAsync(admin);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, admin.EMAIL),
                //new Claim(JwtRegisteredClaimNames.NameId, admin.ID_ADMIN)
            };

            foreach (var role in roles)
            {
                if (!role.Contains("NoRoles"))
                    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
