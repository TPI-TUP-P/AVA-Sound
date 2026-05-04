namespace Application.DTOs.Review.Request;

public class CreateRequest
{
    public Guid IdUser { get; init; }

    public Guid IdSong { get; init; }

    public string? Comment { get; set; }

    public DateTime DateCreated { get; set; }

}