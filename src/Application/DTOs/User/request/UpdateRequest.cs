namespace Application.DTOs.User.Request;

public class UpdateRequest
{
    public string? Name {get; init;}
    public string? Surname {get; init;}
    public string? Email { get; init;}
    public string? Password { get; init;}
    public bool IsArtist {get; init;}
    public DateTime DateRegister { get; init;}
    public string? Role { get; init;}

    public UpdateRequest(string name, string surname, string email, string password, bool isArtist, string role)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        IsArtist = isArtist;
        Role = role;
        DateRegister = DateTime.Now;
    }
    
}