
using Domain.Exceptions;

namespace Domain.Entities;


public class Song
{
    public Guid Id {get; init;}
    public Guid IdArtist {get; init;}
    public Guid? IdAlbum {get; set;}
    
    public User Artist {get; set;}

    public string Title {get; set;}
    public string Gender {get; set;}
    public string Duration{get; set;}
    public string AudioBig {get; set;}
    public DateTime DateUpload {get; init;}
    public int Views {get; set;}




    public Song( Guid idArtist, Guid? idAlbum, string title, string gender, string duration, string audioBig)
    {
        ValidateProperties(idArtist,idAlbum , title, gender, duration, audioBig);
        
        Id = Guid.NewGuid();
        IdArtist=idArtist;
        IdAlbum=idAlbum;
        Title=title;
        Gender=gender;
        Duration=duration;
        AudioBig=audioBig;
        DateUpload=DateTime.Now;
        Views=0;
    }




    public void AddView()
    {
        Views++;
    }

    public void RemoveFromAlbum()
    {
        IdAlbum = null;
    }

    private void ValidateProperties(Guid idArtist, Guid? idAlbum, string title, string gender, string duration, string audioBig)
    {
        if (idArtist == Guid.Empty)
            throw new FieldEmptyExcepction("artist is required");

        if(idAlbum.HasValue)
        {
            if(idAlbum.Value == Guid.Empty)
            {
                throw new FieldEmptyExcepction("AlbumId");
            }
            
        }

        if (string.IsNullOrWhiteSpace(title))
            throw new FieldEmptyExcepction("title");

        if (string.IsNullOrWhiteSpace(gender))
            throw new FieldEmptyExcepction("gender");

        if (string.IsNullOrWhiteSpace(duration))
            throw new FieldEmptyExcepction("duration");

        if (string.IsNullOrWhiteSpace(audioBig))
            throw new FieldEmptyExcepction("Audio");

    }

public void UpdateInfo(string title, string gender, string duration, string audioBig)
{
    if (string.IsNullOrWhiteSpace(title))
        throw new FieldEmptyExcepction("Title");

    if (string.IsNullOrWhiteSpace(gender))
        throw new FieldEmptyExcepction("Gender");

    if (string.IsNullOrWhiteSpace(duration))
        throw new FieldEmptyExcepction("Duration");

    if (string.IsNullOrWhiteSpace(audioBig))
        throw new FieldEmptyExcepction("Audio File");

    Title = title;
    Gender = gender;
    Duration = duration;
    AudioBig = audioBig;
}

    

}