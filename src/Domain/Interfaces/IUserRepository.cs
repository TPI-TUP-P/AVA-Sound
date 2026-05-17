using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<List<User>> GetAll( int page, int PageSize);
    Task<int> Count();
}