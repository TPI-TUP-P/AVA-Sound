namespace Domain.Entities;


public class Statistic
{
  public  Guid Id { get; init; }
  public  Guid IdUser { get; init; }
   public Guid SongTop {get; set;}
   public string? FavoriteGender {get; set;}
   
   public int TotalReproductions {get; set;}


    private Statistic(){}


    public  Statistic (Guid id, Guid idUser, Guid songTop, string? favoriteGender, int totalReproductions)
    {
        
        ValidateProperties(idUser, songTop, favoriteGender, totalReproductions);
        Id = id;
        IdUser = idUser;
        SongTop = songTop;
        FavoriteGender = favoriteGender;
        TotalReproductions = totalReproductions;
    }

    private void ValidateProperties( Guid idUser, Guid songTop, string? favoriteGender, int totalReproductions)
    {
            if(idUser == Guid.Empty)
        {
            throw new Exception("The Iduser cannot be empty");
        }
        if(songTop == Guid.Empty)
        {
            throw new Exception("The IdSongTop cannot be empty");
        }
        if (string.IsNullOrWhiteSpace(favoriteGender))
        {
            throw new Exception("The Favorite field cannot be left blank");
        }
        if (totalReproductions < 0)
        {
            throw new Exception("The total number of views cannot be negative");
        }
        

    }
    


    
}