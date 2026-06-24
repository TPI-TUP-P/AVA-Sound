using Domain.Entities;

namespace Domain.Interfaces;

public interface IInfoUserRepository : IRepository<InfoUser>
{
    new Task<InfoUser?> GetById(Guid IdUser, CancellationToken cancellationToken);
}