using Domain.Enums;

namespace Application.DTOs.User.Response;

public class GetByIdResponse
{
    public Guid Id {get; init;}
    public string? Name {get; set;}
    public string? Surname {get; set;}
    public string? Email { get; set;}
    public bool IsArtist {get; set;}
    public DateTime DateRegister { get; init;}
    public UserRole? Role { get; set;}
    
    public GetByIdResponse(Guid id, string? name, string? surname, string? email, bool isArtist, DateTime dateRegister, UserRole? role)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        IsArtist = isArtist;
        DateRegister = dateRegister;
        Role = role;
    }
}