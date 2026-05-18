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