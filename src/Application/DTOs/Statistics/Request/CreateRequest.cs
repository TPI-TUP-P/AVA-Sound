namespace Application.DTOs.Statistic.Request;
public class CreateRequest
{
   public Guid IdUser { get; init; }
   public Guid SongTop {get; set;}
   public string? FavoriteGender {get; set;}
   public int TotalReproductions {get; set;}
}