using Domain.Entities;

namespace Domain.Interfaces;

public interface IStatisticRepository : IRepository<Statistic>
{
   Task<List<Statistic>> GetAll();
  // Task<List<Song>> GetTopSongs (CancellationToken cancellationToken);

  Task<Statistic> GetByIdUser(Guid IdUser, CancellationToken cancellationToken);

  Task UpdateStatistic (Statistic statistic, CancellationToken cancellationToken);

  // Task<IEnumerable<Song>> GetTopArtist(CancellationToken cancellationToken);

    
}