namespace Application.DTOs.Album.Response;


public class GetAllResponse
{
    public Guid Id {get;  init;}
    public string? Title {get; set;}
    public DateTime ReleasteDate {get; set;}
    public string? FrontPage {get;set;}



    public GetAllResponse(Guid id, string? title, DateTime releasteDate, string? frontPage)
    {
        Id = id;
        Title = title;
        ReleasteDate = releasteDate;
        FrontPage = frontPage;
    }
    
}