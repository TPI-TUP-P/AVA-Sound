using Domain.Entities;
namespace Domain.Interfaces;
public interface IReproductionsListRepository :IRepository<ReproductionsList>
{
    // Task DeleteSong(Guid id);
    // Task AddSong(Guid id);
    // Task<List<ReproductionsList>> GetAll(CancellationToken cancellationToken);
    Task<ReproductionsList> AddSong(Guid id, Song song);
    Task<ReproductionsList> RemoveSong(Guid id, Song song);
    Task<List<ReproductionsList>> GetByIdUser(Guid idUser, CancellationToken cancellationToken);
}
