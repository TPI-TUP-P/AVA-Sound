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

    public async Task Create(InfoUser infouser)
    {
        await _context.InfoUsers.AddAsync(infouser);
        await _context.SaveChangesAsync();
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

    public async Task Update(InfoUser infouser)
    {
        var existing = await _context.InfoUsers.FindAsync(infouser.Id);
        if (existing is null)
        {
            return;
        }
        existing.ProfilePicture = infouser.ProfilePicture;
        existing.Biography = infouser.Biography;
        existing.Country = infouser.Country;
        await _context.SaveChangesAsync();
    }
}