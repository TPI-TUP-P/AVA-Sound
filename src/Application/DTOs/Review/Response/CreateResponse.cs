namespace Application.DTOs.Review.Response;

public class CreateResponse
{
    public Guid IdUser { get; private set; }

    public Guid IdSong { get; private set; }

    public string? Comment { get; set; }
    public DateTime DateCreated { get; set; }
}