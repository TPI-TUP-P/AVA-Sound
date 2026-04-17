namespace Domain.Interfaces;

public interface IStatistic
{
   Guid Id {get;}
   Guid IdUser {get;}
   Guid SongTop {get;}
   string? FavoriteGender {get;}
   int TotalReproductions {get;}

    
}