namespace Application.DTOs.Album.Response;


public class CreateResponse
{
    public Guid IdArtist {get; private set;}
    public string? Title {get; set;}
    public DateTime ReleasteDate {get; set;}
    public string? FrontPage {get;set;}
    public string? Description {get;set;}


    public CreateResponse(Guid idArtist, string? title, DateTime releasteDate, string? frontPage, string? description)
    {
        IdArtist = idArtist;
        Title = title;
        ReleasteDate = releasteDate;
        FrontPage = frontPage;
        Description = description;
    }
}
