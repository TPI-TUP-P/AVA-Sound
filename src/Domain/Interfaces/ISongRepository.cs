using Domain.Entities;
namespace Domain.Interfaces;
public interface ISongRepository : IRepository<Song>
{
    Task<List<Song>> GetAll(int Page,int PageSize,CancellationToken cancellationToken);
    Task <int> Count();
    Task Add(Song song);
}
