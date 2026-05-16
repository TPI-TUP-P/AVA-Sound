using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<List<User>> GetAll(CancellationToken cancellationToken);
    Task<User> GetByEmail(string email, CancellationToken cancellationToken);

}