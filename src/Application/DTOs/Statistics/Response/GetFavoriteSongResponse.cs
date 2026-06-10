namespace Application.DTOs.Statistic.Response;

public class  GetFavoriteSongResponse {
       public Guid IdArtist {get; init;}
    public Guid? IdAlbum {get; init;}
    public string? Title {get; set;}
    public string? Gender {get; set;}
    public string? Duration{get; set;}
    public string? AudioBig {get; set;}
    public DateTime DateUpload {get; set;}
    public int Views {get; set;}

    public GetFavoriteSongResponse(Guid idArtist, Guid? idAlbum, string title, string gender, string duration, string audioBig,DateTime dateUpload, int views)
    {
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