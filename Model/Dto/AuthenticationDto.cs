namespace Model.Dto;

public class AuthenticationDto
{
    public string Code { get; set; }
    public bool IsDevSource { get; set; }

    public AuthenticationDto(string code, bool isDevSource)
    {
        Code = code;
        IsDevSource = isDevSource;
    }
}