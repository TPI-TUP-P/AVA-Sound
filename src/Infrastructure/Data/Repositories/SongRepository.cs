using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class SongRepository : ISongRepository
{
    private readonly ApplicationContext _context;

    public SongRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Song> GetById(Guid id, CancellationToken cancellationToken)
    {
        var song = await _context.Songs.FindAsync(id, cancellationToken);
        if (song == null)
            throw new KeyNotFoundException($"La cancion con el ID {id} no fue encontrado.");

        return song;
    }

    public async Task Add (Song song)
    {
        await _context.Songs.AddAsync(song);
        await _context.SaveChangesAsync();
    }

<<<<<<< HEAD
=======


>>>>>>> d66b2d5cdc48eab7f7f26792940d37042e60bd29
    public async Task<List<Song>> GetAll(int Page,int PageSize,CancellationToken cancellationToken)
    {
        return await _context.Songs
            .OrderByDescending(s => s.DateUpload)
            .Skip((Page-1)*PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> Count()
    {
        return await _context.Users.CountAsync();
    }

    public async Task<Song> Create(Song song, CancellationToken cancellationToken)
    {
        _context.Songs.Add(song);
        await _context.SaveChangesAsync();
        return song;
    }

    public async Task<Song> Update(Song song, CancellationToken cancellationToken)
    {
        _context.Songs.Update(song);
        await _context.SaveChangesAsync(cancellationToken);
        return song;
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var song = await _context.Songs.FindAsync(id);

        if (song != null)
        {
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}