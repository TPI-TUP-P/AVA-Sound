namespace Application.DTOs.Song.Response;

public class CreateResponse
{
    public Guid Id {get; init;}
    public Guid IdArtist {get; init;}
    public Guid IdAlbum {get; set;}
    public string Title {get; set;}
    public string Gender {get; set;}
    public string Duration{get; set;}
    public string AudioBig {get; set;}
    public DateTime DateUpload {get; init;}
    public int Views {get; set;}

    public CreateResponse(Guid id, Guid idArtist, Guid idAlbum, string title, string gender, string duration, string audioBig, DateTime dateUpload, int views)
    {
        Id = id;
        IdArtist = idArtist;
        IdAlbum = idAlbum;
        Title = title;
        Gender = gender;
        Duration = duration;
        AudioBig = audioBig;
        DateUpload = dateUpload;
        Views = views;
    }
}