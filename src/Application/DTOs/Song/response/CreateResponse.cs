namespace Application.DTOs.Song.Response;

public class CreateResponse
{
    public Guid IdArtist {get; init;}
    public Guid IdAlbum {get; init;}
    public string? Title {get; set;}
    public string? Gender {get; set;}
    public string? Duration{get; set;}
    public string? AudioBig {get; set;}
    public DateTime DateUpload {get; set;}
    public int Views {get; set;}
}