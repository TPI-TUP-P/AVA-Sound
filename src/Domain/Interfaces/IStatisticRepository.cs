using Domain.Entities;

namespace Domain.Interfaces;

public interface IStatisticRepository : IRepository<Statistic>
{
   Task<List<Statistic>> GetAll();
  Task<List<Song>> GetTopSongs (CancellationToken cancellationToken);

  Task<List<Song>> GetTopArtist(CancellationToken cancellationToken);

    
}