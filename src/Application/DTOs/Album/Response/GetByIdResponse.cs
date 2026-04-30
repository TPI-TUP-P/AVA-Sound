namespace Application.DTOs.Album.Response;



public class GetByIdResponse
{
    public Guid Id {get;  init;}
    public Guid IdArtist {get; init;}
    public string? Title {get; set;}
    public DateTime ReleasteDate {get; set;}
    public string? FrontPage {get;set;}
    public string? Description {get;set;}


}