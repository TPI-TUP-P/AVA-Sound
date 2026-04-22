using Domain.Entities;

namespace Domain.Interfaces;

public interface IStatisticRepository : IRepository<Statistic>
{
   Task<List<Statistic>> GetAll();
  

    
}