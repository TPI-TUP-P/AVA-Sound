namespace Domain.Entities;


public class Statistic
{
  public  Guid Id { get; private set; }
  public  Guid IdUser { get;private set; }
   public Guid SongTop {get;private set;}
   public string? FavoriteGender {get;private set;}
   public int TotalReproductions {get;private set;}




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
            throw new Exception("El Iduser no puede ser vacio");
        }
        if(songTop == Guid.Empty)
        {
            throw new Exception("El IdSongTop no puede ser vacio");
        }
        if (string.IsNullOrWhiteSpace(favoriteGender))
        {
            throw new Exception("El genero favorito no puede estar vacio");
        }
        if (totalReproductions < 0)
        {
            throw new Exception("El total de reproducciones no puede ser negativo");
        }
        

    }
    


    
}