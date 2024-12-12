using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

public class CustomClaimsTransformation : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
        {
            // Add custom claims here
            if (!principal.HasClaim(c => c.Type == "CustomClaim"))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                identity.AddClaim(new Claim(ClaimTypes.Email, "user@example.com"));
            }

            // Add more claims as needed
        }

        return Task.FromResult(principal);
    }
}
