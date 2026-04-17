using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetById(Guid id);
    Task Add(User user);
    Task Update(User user);
    Task Delete(Guid id);
}