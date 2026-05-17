namespace Application.DTOs.User.Response;

public class UpdateResponse
{
    public string? Name {get; init;}
    public string? Surname {get; init;}
    public string? Email { get; init;}
    public string? Password { get; init;}
    public bool IsArtist {get; init;}
    public DateTime DateRegister { get; init;}
    public string? Role { get; init;}
}