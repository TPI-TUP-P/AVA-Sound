namespace Application.DTOs.Album.Response;
using Domain.Entities;


public class GetByIdResponse
{
    public Guid Id {get;  init;}
    public Guid IdArtist {get; init;}
    public string? Title {get; set;}
    public DateTime ReleasteDate {get; set;}
    public string? FrontPage {get;set;}
    public string? Description {get;set;}

    public List<Song> Songs { get; set; }        
    
   

    public GetByIdResponse(Guid id, Guid idArtist, string? title, DateTime releasteDate, string? frontPage, string? description, List<Song> songs
    )
    {
        Id = id;   
        IdArtist = idArtist;
        Title = title;
        ReleasteDate = releasteDate;
        FrontPage = frontPage;
        Description = description;
        Songs = songs;
    

    }
       
        
        
}