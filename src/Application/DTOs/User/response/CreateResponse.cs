namespace Application.DTOs.User.Response;

public class CreateResponse
{
    public Guid Id {get; init;}
    public string? Name {get; set;}
    public string? Surname {get; set;}
    public string? Email { get; set;}
    public bool IsArtist {get; set;}
    public DateTime DateRegister { get; init;}
    public string? Role { get; init;}
}