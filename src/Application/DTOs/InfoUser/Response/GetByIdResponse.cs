namespace Application.DTOs.InfoUser.Response;

public class GetByIdResponse
{
    public Guid Id { get; init; }
    public Guid IdUser { get; init; }

    public string? ProfilePicture { get; set; }

    public string? Biography { get; set; }

    public string? Country { get; set; }

    public GetByIdResponse(Guid id, Guid idUser, string profilePicture, string biography, string country)
    {
        Id = id;
        IdUser = idUser;
        ProfilePicture = profilePicture;
        Biography = biography;
        Country = country;
    }
}