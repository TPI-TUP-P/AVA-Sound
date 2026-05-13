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

    public async Task<Song> GetById(Guid id)
    {
        var song = await _context.Songs.FindAsync(id);
        if (song == null)
            throw new KeyNotFoundException($"La cancion con el ID {id} no fue encontrado.");

        return song;
    }


    public async Task<List<Song>> GetAll()
    {
        return await _context.Songs
            .OrderByDescending(s => s.DateUpload)
            .Take(30)
            .ToListAsync();
    }

    public async Task<Song> Create(Song song, CancellationToken cancellationToken)
    {
        _context.Songs.Add(song);
        await _context.SaveChangesAsync();
        return song;
    }

    public async Task<Song> Update(Song song)
    {
        _context.Songs.Update(song);
        await _context.SaveChangesAsync();
        return song;
    }

    public async Task Delete(Guid id)
    {
        var song = await _context.Songs.FindAsync(id);

        if (song != null)
        {
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
        }
    }
}