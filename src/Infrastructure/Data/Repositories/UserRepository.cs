using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<User> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(id, cancellationToken);
        if (user == null)
            throw new Exception($"El usuario con el ID {id} no fue encontrado.");

        return user;
    }

    


    public async Task<List<User>> GetAll(int page, int pageSize)
    {
        return await _context.Users
        .OrderBy(x => x.Name)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    }   

    public async Task<int> Count()
    {
        return await _context.Users.CountAsync();
    public async Task<User> GetByEmail(string email, CancellationToken cancellationToken)
    {   
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        
    }

    public async Task<List<User>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Users
            .OrderByDescending(s => s.DateRegister)
            .Take(30)
            .ToListAsync(cancellationToken);
    }

    public async Task<User> Create(User user, CancellationToken cancellationToken)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User> Update(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(id);

        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}