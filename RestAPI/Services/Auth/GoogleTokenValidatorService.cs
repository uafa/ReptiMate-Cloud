using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ReptiMate_Cloud.Services.Auth;

public class GoogleTokenValidatorService : ISecurityTokenValidator
{
    private readonly string clientId;
    private readonly JwtSecurityTokenHandler tokenHandler;
    
    public GoogleTokenValidatorService(string clientId)
    {
        this.clientId = clientId;
        tokenHandler = new JwtSecurityTokenHandler();
    }
    
    public bool CanValidateToken => true;

    public int MaximumTokenSizeInBytes { get; set; } = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;

    public bool CanReadToken(string securityToken)
    { 
        return tokenHandler.CanReadToken(securityToken);
    }

    public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters,
        out SecurityToken validatedToken)
    {
        //Later on if the token is validated it will be assigned to this 
        validatedToken = null;
        try
        {
            //This code delegates to Google to validate the token, if it fails it throws an exception
            var payload = GoogleJsonWebSignature.ValidateAsync(securityToken,
                new GoogleJsonWebSignature.ValidationSettings() { Audience = new[] { clientId } }).Result;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, payload.Name),
                new Claim(ClaimTypes.Name, payload.Name),
                new Claim(JwtRegisteredClaimNames.FamilyName, payload.FamilyName),
                new Claim(JwtRegisteredClaimNames.GivenName, payload.GivenName),
                new Claim(JwtRegisteredClaimNames.Email, payload.Email),
                new Claim(JwtRegisteredClaimNames.Sub, payload.Subject),
                new Claim(JwtRegisteredClaimNames.Iss, payload.Issuer),
            };
            
            //If the payload does not throw an InvalidJwtException we assign the token here
            validatedToken = new JwtSecurityToken(securityToken);

            var principle = new ClaimsPrincipal();
            principle.AddIdentity(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
            return principle;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}