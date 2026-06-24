namespace Application.DTOs.InfoUser.Response;

public class UpdateResponse
{
    public Guid IdUser { get; init; }
    public string? ProfilePicture { get; set; }

    public string? Biography { get; set; }

    public string? Country { get; set; }

    public UpdateResponse(Guid idUser, string profilePicture, string biography, string country)
    {
        IdUser = idUser;
        ProfilePicture = profilePicture;
        Biography = biography;
        Country = country;

    }
}
