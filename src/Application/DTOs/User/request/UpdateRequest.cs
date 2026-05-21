namespace Application.DTOs.User.Request;

public class UpdateRequest
{
    public string? Name {get; set;}
    public string? Surname {get; set;}
    public string? Email { get; set;}
    public string? Password { get; set;}
    public bool IsArtist {get; set;}
   


    public UpdateRequest(string? name, string? surname, string? email, string? password, bool isArtist)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        IsArtist = isArtist;
    }
    
}