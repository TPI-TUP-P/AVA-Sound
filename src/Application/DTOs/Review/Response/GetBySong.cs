namespace Application.DTOs.Review.Response;

public class GetBySongResponse
{
    public Guid IdUser { get; init; }

    public Guid IdSong { get; init; }

    public string? Comment { get; set; }

    public DateTime DateCreated { get; set; }
}