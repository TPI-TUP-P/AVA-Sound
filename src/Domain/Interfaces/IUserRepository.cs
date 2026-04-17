using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetById(Guid id);
    Task<List<User>> GetAll();
    Task Add(User user);
    Task Update(User user);
    Task Delete(Guid id);
    Task<List<User>> GetByRole(string role);
}