namespace Application.DTOs.InfoUser.Response;

public class CreateResponse
{
    public Guid Id { get; set; }
    public Guid IdUser { get; init; }

    public string? ProfilePicture { get; set; }

    public string? Biography { get; set; }

    public string? Country { get; set; }

    public CreateResponse(Guid id, Guid idUser, string profilePicture, string biography, string country)
    {
        Id = id;
        IdUser = idUser;
        ProfilePicture = profilePicture;
        Biography = biography;
        Country = country;

    }
}