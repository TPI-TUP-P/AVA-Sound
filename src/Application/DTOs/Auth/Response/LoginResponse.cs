using Domain.Enums;

namespace Application.DTOs.Auth.Response;


public class LoginResponse
{
    public string Token { get; init; }
    public string Email { get; init; }
    public UserRole? Role { get; init; }

    public LoginResponse(string token, string email, UserRole? role)
    {
        Token = token;
        Email = email;
        Role = role;
    }
}