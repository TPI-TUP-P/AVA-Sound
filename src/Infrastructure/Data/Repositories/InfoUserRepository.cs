namespace Infrastructure.Data.Repositories;

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

public class InfoUserRepository : IInfoUserRepository
{
    private readonly ApplicationContext _context;

    public InfoUserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<InfoUser?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.InfoUsers
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<InfoUser> Create(InfoUser infouser, CancellationToken cancellationToken)
    {
        await _context.InfoUsers.AddAsync(infouser, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return infouser;
    }

    public async Task Delete(Guid Id, CancellationToken cancellationToken)
    {
        var info = await _context.InfoUsers.FindAsync(Id);
        if (info != null)
        {
            _context.InfoUsers.Remove(info);
        }
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<InfoUser> Update(InfoUser infouser, CancellationToken cancellationToken)
    {
        var existing = await _context.InfoUsers.FindAsync(infouser.Id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"User information with ID: {infouser.Id} was no found");
        }
        existing.ProfilePicture = infouser.ProfilePicture;
        existing.Biography = infouser.Biography;
        existing.Country = infouser.Country;
        await _context.SaveChangesAsync(cancellationToken);
        return existing;

    }
}