using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace ReptiMate_Cloud.Services.Auth;

public class JwtTokenService
{
    public static string GetClaimValueFromJwt(string claimType, string token)
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        return jwt.Claims.First(c => c.Type == claimType).Value;
    }
}