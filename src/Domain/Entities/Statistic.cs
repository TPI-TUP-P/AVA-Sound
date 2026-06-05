using Domain.Objects.Statistics;

namespace Domain.Entities;


public class Statistic
{
  public  Guid Id { get; init; }
  public  Guid IdUser { get; init; }

    public List<SongReproduction> Reproductions { get; set; } = new();


    public  Statistic (Guid idUser)
    {
        
        Id =Guid.NewGuid();
        IdUser = idUser;
     

        ValidateProperties(idUser);

    }

    private void ValidateProperties( Guid idUser)
    {
            if(idUser == Guid.Empty)
        {
            throw new Exception("The Iduser cannot be empty");
        }

        
   
   

    }



    public string  GetFavoriteGender()
    {
        var gender = Reproductions.OrderByDescending(s=> s.ViewsCount).FirstOrDefault();
        return gender!.Gender;


    } 


    public Guid GetFavoriteSong()
    {
        var favoriteSong = Reproductions.OrderByDescending(s=> s.ViewsCount).FirstOrDefault();
        return favoriteSong!.IdSong;


    }



    public void RegisterViewGender(Guid idSong, string gender)
    {
        var existingSong = Reproductions.FirstOrDefault(s=> s.IdSong == idSong);

      if( existingSong == null)
      {
        var songReproduction = new SongReproduction
        {
            IdSong = idSong,
            Gender = gender,
            ViewsCount = 1
        };
        Reproductions.Add(songReproduction);
      }
      else
        {
            existingSong.ViewsCount++;
        }
        
    }


    
    
}