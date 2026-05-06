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

    public async Task<InfoUser> GetById(Guid Id)
    {
        return await _context.InfoUsers.FirstAsync(i => i.IdUser == Id);
    }

    public async Task<InfoUser> Create(InfoUser infouser, CancellationToken cancellationToken)
    {
        await _context.InfoUsers.AddAsync(infouser, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return infouser;
    }

    public async Task Delete(Guid Id)
    {
        var info = await _context.InfoUsers.FindAsync(Id);
        if (info != null)
        {
            _context.InfoUsers.Remove(info);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<InfoUser> Update(InfoUser infouser)
    {
        var existing = await _context.InfoUsers.FindAsync(infouser.Id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"La información del usuario con el ID {infouser.Id} no fue encontrada.");
        }
        existing.ProfilePicture = infouser.ProfilePicture;
        existing.Biography = infouser.Biography;
        existing.Country = infouser.Country;
        await _context.SaveChangesAsync();
        return infouser;

    }
}