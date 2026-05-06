using Domain.Entities;

namespace Application.Interfaces;

public interface IReproductionListService
{
    Task<ReproductionsList?> GetById(Guid id);
    Task<ReproductionsList> Create(ReproductionsList reproductionsList, CancellationToken cancellationToken);
    Task<ReproductionsList> AddSong(Guid id, Song song);
    Task<ReproductionsList> DeleteSong(Guid id, Song song);
    Task Delete(Guid id);
}