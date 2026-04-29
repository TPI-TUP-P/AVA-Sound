namespace Application.DTOs.Album;


public class UpdateAlbumDto
{
    
    public Guid IdArtist { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? FrontPage { get; set; }
    public string? Description { get; set; }
}