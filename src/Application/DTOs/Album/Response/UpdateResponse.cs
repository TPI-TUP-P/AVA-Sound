namespace Application.DTOs.Album.Response;


public class UpdateResponse
{
    public Guid Id {get;  init;}
    public Guid IdArtist {get; init;}
    public string? Title {get; set;}
    public DateTime ReleasteDate {get; set;}
    public string? FrontPage {get;set;}
    public string? Description {get;set;}


    public UpdateResponse(Guid id, Guid idArtist, string? title, DateTime releasteDate, string? frontPage, string? description)
    {
        Id = id;
        IdArtist = idArtist;
        Title = title;
        ReleasteDate = releasteDate;
        FrontPage = frontPage;
        Description = description;
        
        }
}