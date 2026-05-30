using Domain.Entities;
namespace Domain.Interfaces;
public interface ISongRepository : IRepository<Song>
{
    Task<List<Song>> GetAll(int Page,int PageSize,CancellationToken cancellationToken);
    Task <int> Count();
<<<<<<< HEAD
=======
    Task Add(Song song);
>>>>>>> d66b2d5cdc48eab7f7f26792940d37042e60bd29
}
