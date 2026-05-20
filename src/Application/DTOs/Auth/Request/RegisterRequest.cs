namespace Application.DTOs.Auth.Request;


public class RegisterRequest
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set;}
    public string? Password { get; set; }
    public bool IsArtist { get; set; }

    public string? Role { get; set; }
    
}