namespace Application.DTOs.User.response;

public class GetByIdResponse
{
    public string? Name {get; init;}
    public string? Surname {get; init;}
    public string? Email { get; init;}
    public bool IsArtista {get; init;}
    public DateTime DateRegister { get; init;}
    public string? Role { get; init;}
}