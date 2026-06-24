// using Domain.Enums;

namespace Application.DTOs.User.Response;

public class GetAllArtistResponse
{
    public Guid Id {get; init;}
    public string? Name {get; set;}
    public string? Surname {get; set;}
    public GetAllArtistResponse(Guid id, string? name, string? surname)
    {
        Id = id;
        Name = name;
        Surname = surname;
       
    }
}