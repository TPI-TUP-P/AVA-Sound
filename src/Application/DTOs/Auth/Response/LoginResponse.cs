namespace Application.DTOs.Auth.Response;


public class LoginResponse
{
    public string? Token { get; init; }
    public string? Email { get; init; }
    public string? Role { get; init; }

    public LoginResponse(string? token, string? email, string? role)
    {
        Token = token;
        Email = email;
        Role = role;
    }
}