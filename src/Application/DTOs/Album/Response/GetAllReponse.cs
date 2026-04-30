namespace Application.DTOs.Album.Response;


public class GetAllResponse
{
    public Guid Id {get;  init;}
    public string? Title {get; set;}
    public DateTime ReleasteDate {get; set;}
    public string? FrontPage {get;set;}



}