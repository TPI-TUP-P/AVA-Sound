using Domain.Entities;

namespace Domain.Interfaces;

public interface IInfoUserRepository : IRepository<InfoUser>
{
    Task<InfoUser?> GetById(Guid IdUser, CancellationToken cancellationToken);
}