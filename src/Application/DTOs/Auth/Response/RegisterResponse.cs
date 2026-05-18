namespace Application.DTOs.Auth.Response;


public class RegisterResponse
{
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public string? Email { get; init; }
    public bool IsArtist { get; init; }
    public string? Role { get; init; }


    public RegisterResponse(string? name, string? surname, string? email, bool isArtist, string? role)
    {
        Name = name;
        Surname = surname;
        Email = email;
        IsArtist = isArtist;
        Role = role;
    }
    
}

