namespace Application.DTOs.Statistic.Response;

public class GetTopSongsResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string Duration { get; set; } = null!;
    public int Views { get; set; }
    public Guid IdArtist { get; set; }
    public Guid? IdAlbum { get; set; }

    public GetTopSongsResponse() { }

    public GetTopSongsResponse(Guid id, string title, string gender, string duration, int views, Guid idArtist, Guid? idAlbum)
    {
        Id = id;
        Title = title;
        Gender = gender;
        Duration = duration;
        Views = views;
        IdArtist = idArtist;
        IdAlbum = idAlbum;
    }
}
