namespace Application.DTOs.Song.Response;
public class PagerSongResponse<T>
{
    public List<T> Songs {get; set;}=new();
    public int Page {get; set;}
    public int PageSize {get; set;}
    public int SongTotal {get; set;}
    public int PageTotal {get; set;}

    

}