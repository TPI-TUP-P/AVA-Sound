namespace Application.DTOs.Statistic.Request;


public class UpdateRequest
{
    
    public Guid IdUser { get; init; }
    public Guid SongTop {get; set;}
    public string? FavoriteGender {get; set;}

    public Dictionary<string, int> TotalReproductionByGender {get; set;}
}