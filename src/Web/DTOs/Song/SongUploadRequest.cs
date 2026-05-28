namespace Web.DTOs.Song;


public class SongUploadRequest
{
    public Guid IdAlbum { get; set; }
    public string Title { get; set; } = "";
    public string Gender { get; set; } = "";
    public string Duration { get; set; } = "";
    public IFormFile AudioFile { get; set; } = null!; // ← solo en Web
}