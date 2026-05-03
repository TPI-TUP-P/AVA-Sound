namespace Application.DTOs.Statistic.Response;


public class GetByIdResponse
{
    public Guid Id { get; init;}
    public Guid IdUser { get; init; }
    public Guid SongTop { get;  set;}
    public string? FavoriteGender { get;set;  }
    public int TotalReproductions { get; set; }


    public GetByIdResponse(Guid id, Guid idUser, Guid songTop, string? favoriteGender, int totalReproductions)
    {
        Id = id;
        IdUser = idUser;
        SongTop = songTop;
        FavoriteGender = favoriteGender;
        TotalReproductions = totalReproductions;
    }


}