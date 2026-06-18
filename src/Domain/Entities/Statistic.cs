using Domain.Exceptions;
using Domain.Objects.Statistics;

namespace Domain.Entities;


public class Statistic
{
    public  Guid Id { get; init; }
    public  Guid IdUser { get; init; }
    public List<SongReproduction> Reproductions { get; set; } = [];


    public  Statistic (Guid idUser)
    {
        
        ValidateProperties(idUser);

        Id =Guid.NewGuid();
        IdUser = idUser;
    }

    private static void ValidateProperties( Guid idUser)
    {
            if(idUser == Guid.Empty)
        {
            throw new FieldEmptyExcepction("IdUser");
        }
    }



    public string  GetFavoriteGender(IEnumerable<Song>songs)
    {
        var gender = this.Reproductions.Join(songs, r=> r.IdSong, s=> s.Id, (r,s)=> new {s.Gender,r.ViewsCount}).GroupBy(s=> s.Gender).Select(g=> new
        {
            Genre = g.Key,
            Count = g.Sum(x=> x.ViewsCount)
        }).OrderByDescending(x=> x.Count)
        .FirstOrDefault();

        return gender?.Genre ?? "Unknown";
    } 


    public Guid GetFavoriteSong()
    {
        var favoriteSong = Reproductions.OrderByDescending(s=> s.ViewsCount).FirstOrDefault();
        return favoriteSong!.IdSong;


    }



    public void RegisterViewGender(Guid idSong)
    {
        var existingSong = Reproductions.FirstOrDefault(s=> s.IdSong == idSong);

      if( existingSong == null)
      {
        var songReproduction = new SongReproduction
        {
            IdSong = idSong,
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