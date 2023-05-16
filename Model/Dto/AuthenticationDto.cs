namespace Model.Dto;

public class AuthenticationDto
{
    public string Code { get; set; }

    public AuthenticationDto(string code)
    {
        Code = code;
    }
}