using Domain.Entities;
namespace Domain.Interfaces;
public interface ISongRepository
{
    Task<Song?> GetById(Guid id);
    Task Add(Song song);
    Task Update(Song song);
    Task Delete(Guid id);
}
