namespace Application.DTOs.Review.Response;

public class CreateResponse
{
    public Guid IdUser { get; init; }

    public Guid IdSong { get; init; }

    public string? Comment { get; set; }
    public DateTime DateCreated { get; set; }
}