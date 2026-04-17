
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


    public Song( Guid idArtist, Guid idAlbum, string title, string gender, string duration, string audioBig, DateTime dateUpload, int views)
    {
        ValidateProperties(idArtist, idAlbum, title, gender, duration, audioBig, dateUpload, views);
        Id = Guid.NewGuid();
        IdArtist=idArtist;
        IdAlbum=idAlbum;
        Title=title;
        Gender=gender;
        Duration=duration;
        AudioBig=audioBig;
        DateUpload=dateUpload;
        Views=views;
    }

    private void ValidateProperties(Guid idArtist, Guid idAlbum, string title, string gender, string duration, string audioBig, DateTime dateUpload, int views)
    {
        if (idArtist == Guid.Empty)
            throw new Exception("El artista es obligatorio");

        if (idAlbum == Guid.Empty)
            throw new Exception("El álbum es obligatorio");

        if (string.IsNullOrWhiteSpace(title))
            throw new Exception("El título es obligatorio");

        if (string.IsNullOrWhiteSpace(gender))
            throw new Exception("El género es obligatorio");

        if (string.IsNullOrWhiteSpace(duration))
            throw new Exception("La duración es obligatoria");

        if (string.IsNullOrWhiteSpace(audioBig))
            throw new Exception("El archivo de audio es obligatorio");

        if (dateUpload == default)
            throw new Exception("La fecha de subida es obligatoria");

        if (views < 0)
            throw new Exception("Las vistas no pueden ser negativas");
    }


    

}