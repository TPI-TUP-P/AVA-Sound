namespace Application.DTOs.InfoUser.Response;

public class CreateResponse
{
    public Guid IdUser { get; init; }

    public string? ProfilePicture { get; set; }

    public string? Biography { get; set; }

    public string? Country { get; set; }
}