namespace Application.DTOs.Song.Response;

public class GetByUserResponse
{
    public Guid IdArtist {get; init;}
    public Guid IdAlbum {get; set;}
    public string? Title {get; set;}
    public string? Gender {get; set;}
    public string? Duration{get; set;}
    public string? AudioBig {get; set;}
    public DateTime DateUpload {get; init;}
    public int Views {get; set;}
}