namespace Model.Dto;

public class GoogleOAuthDto
{
    public string access_token { get; set; }
    public int expires_in { get; set; }
    public string id_token { get; set; }
    public string scope { get; set; }
    public string token_type { get; set; }
    public string refresh_token { get; set; }

    public GoogleOAuthDto(string access_token, int expires_in, string id_token, string scope, string token_type, string refresh_token)
    {
        this.access_token = access_token;
        this.expires_in = expires_in;
        this.id_token = id_token;
        this.scope = scope;
        this.token_type = token_type;
        this.refresh_token = refresh_token;
    }
}