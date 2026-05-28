namespace Application.DTOs.Album.Request;


public class UpdateRequest
{

    public Guid Id { get; init; }
    
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? FrontPage { get; set; }
    public string? Description { get; set; }
}