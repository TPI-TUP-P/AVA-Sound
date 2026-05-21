namespace Application.DTOs.User.Request;

public class CreateRequest
{
    public string? Name {get; set;}
    public string? Surname {get; set;}
    public string? Email { get; set;}
    public string? Password { get; set;}
    public bool IsArtist {get; set;}
    
}
