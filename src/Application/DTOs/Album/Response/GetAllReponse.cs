namespace Application.DTOs.Album.Response;


public class GetAllResponse
{
    public Guid Id {get;  init;}
    public Guid IdArtist {get; private set;}
    public string? Title {get; set;}
    public DateTime ReleasteDate {get; set;}
    public string? FrontPage {get;set;}



    public GetAllResponse(Guid id, Guid idArtist,string? title, DateTime releasteDate, string? frontPage)
    {
        Id = id;
        IdArtist = idArtist;
        Title = title;
        ReleasteDate = releasteDate;
        FrontPage = frontPage;
    }
    
}