using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<List<User>> GetAll( int page, int pageSize, CancellationToken cancellationToken);
    Task<int> Count();
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);

}