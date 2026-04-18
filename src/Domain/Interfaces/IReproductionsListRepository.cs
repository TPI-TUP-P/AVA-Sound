using Domain.Entities;
namespace Domain.Interfaces;
public interface IReproductionsListRepository
{
    Task <ReproductionsList?> GetById(Guid id);
    Task Add(ReproductionsList reproductionsList);
    Task Update (ReproductionsList reproductionsList);
    Task Delete (Guid id);
}
