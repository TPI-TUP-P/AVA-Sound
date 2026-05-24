namespace Application.DTOs.InfoUser.Request;

public class UpdateRequest
{
    public Guid IdUser { get; init; }
    public string? ProfilePicture { get; set; }

    public string? Biography { get; set; }

    public string? Country { get; set; }
}