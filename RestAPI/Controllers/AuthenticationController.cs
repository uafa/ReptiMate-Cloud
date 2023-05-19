using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;
using System.Text.Json;
using Model;
using ReptiMate_Cloud.Services.Auth;

namespace ReptiMate_Cloud.Services;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IAccountServiceRest accountServiceRest;

    public AuthenticationController(IHttpClientFactory httpClientFactory, IAccountServiceRest accountServiceRest)
    {
        this.httpClientFactory = httpClientFactory;
        this.accountServiceRest = accountServiceRest;
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<string>> Authenticate([FromBody]AuthenticationDto dto)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient();

            httpClient.BaseAddress = new Uri("https://oauth2.googleapis.com");

            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("code", dto.Code),
                new KeyValuePair<string, string>("client_id",
                    "756576377617-0t412r5o9fepmnsso6utp40vgbgdfipg.apps.googleusercontent.com"), //TODO: hide to secure place
                new KeyValuePair<string, string>("client_secret",
                    "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"), //TODO: hide to secure place
                new KeyValuePair<string, string>("redirect_uri", "https://reptimate.netlify.app"),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
            });

            var response = await httpClient.PostAsync("/token", requestContent);

            response.EnsureSuccessStatusCode();

            var responseBodyJson = await response.Content.ReadAsStringAsync();
            var responseBody = JsonSerializer.Deserialize<GoogleOAuthDto>(responseBodyJson);

            if (responseBody == null)
            {
                throw new Exception("Couldn't deserialize Google Auth response");
            }

            var sub = JwtTokenService.GetClaimValueFromJwt("sub", responseBody.id_token);
            var email = JwtTokenService.GetClaimValueFromJwt("email", responseBody.id_token);
            
            if (sub == null || email == null)
            {
                throw new Exception("Didn't receive data from Google Auth response");
            }

            var account = await accountServiceRest.GetAccountAsync(email);

            if (account == null)
            {
                var createdAccount = new Account
                {
                    Email = email,
                    FirstName = JwtTokenService.GetClaimValueFromJwt("given_name", responseBody.id_token) ?? "",
                    LastName = JwtTokenService.GetClaimValueFromJwt("family_name", responseBody.id_token) ?? ""
                };

                await accountServiceRest.RegisterAccountAsync(createdAccount);
            }

            return Created("Authenticate", responseBody);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}