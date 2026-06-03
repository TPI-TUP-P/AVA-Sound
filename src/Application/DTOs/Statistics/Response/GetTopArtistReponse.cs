namespace Application.DTOs.Statistic.Response;


public class GetTopArtistReponse
{
    public Guid IdArtist { get; set; }
    public string NameArtist { get; set; }
    public int TotalSongs { get; set; }
    public int TotalViews { get; set; }

    public GetTopArtistReponse(Guid idArtist, string nameArtist, int totalSongs, int totalViews)
    {
     IdArtist = idArtist;
     NameArtist = nameArtist;
     TotalSongs = totalSongs;
     TotalViews = totalViews;
    }
}