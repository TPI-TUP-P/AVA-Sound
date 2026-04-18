
namespace Domain.Entities;


public class Song
{
    public Guid Id {get; private set;}
    public Guid IdArtist {get; private set;}
    public Guid IdAlbum {get; private set;}
    public string? Title {get; set;}
    public string? Gender {get; set;}
    public string? Duration{get; set;}
    public string? AudioBig {get; set;}
    public DateTime DateUpload {get; set;}
    public int Views {get; set;}


    public Song( Guid idArtist, Guid idAlbum, string title, string gender, string duration, string audioBig, int views)
    {
        ValidateProperties(idArtist, idAlbum, title, gender, duration, audioBig, views);
        Id = Guid.NewGuid();
        IdArtist=idArtist;
        IdAlbum=idAlbum;
        Title=title;
        Gender=gender;
        Duration=duration;
        AudioBig=audioBig;
        DateUpload=DateTime.Now;
        Views=views;
    }

    private void ValidateProperties(Guid idArtist, Guid idAlbum, string title, string gender, string duration, string audioBig, int views)
    {
        if (idArtist == Guid.Empty)
            throw new Exception("artist is required");

        if (idAlbum == Guid.Empty)
            throw new Exception("album is required");

        if (string.IsNullOrWhiteSpace(title))
            throw new Exception("title is required");

        if (string.IsNullOrWhiteSpace(gender))
            throw new Exception("gender is required");

        if (string.IsNullOrWhiteSpace(duration))
            throw new Exception("duration is required");

        if (string.IsNullOrWhiteSpace(audioBig))
            throw new Exception("Audio file is required");

        if (views < 0)
            throw new Exception("Views cannot be negative");
    }


    

}