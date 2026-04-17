using Domain.Entities;

namespace Domain.Interfaces;

public interface IStatisticRepository
{
   Task<Statistic?> GetById(Guid id);
   Task<List<Statistic>> GetAll();
   Task Add(Statistic statistic);
   Task Update(Statistic statistic);
   Task Delete(Guid id);

    
}