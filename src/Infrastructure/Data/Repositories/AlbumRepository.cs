namespace Infrastructure.Data.Repositories;

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AlbumRepository : IAlbumRepository
{
    private readonly ApplicationContext _context;

    public AlbumRepository(ApplicationContext context)
    {
        _context = context;
    }
    // private static List<Album> _albums = new();
    public async Task<List<Album>> GetAll()
    {
        return await _context.Albums.ToListAsync();
    }
    public async Task<Album> Update(Album album, CancellationToken cancellationToken)
    {
        var existing = await _context.Albums.FindAsync(album.Id);

        if (existing == null)
        {
            throw new KeyNotFoundException($"El album con el ID {album.Id} no fue encontrado.");
        }

        existing.Title = album.Title;
        existing.Description = album.Description;
        existing.ReleasteDate = album.ReleasteDate;
        existing.FrontPage = album.FrontPage;

        await _context.SaveChangesAsync(cancellationToken);
        return album;
    }

    public async Task<Album> AddSong(Guid id, Song song)
    {
        var album = await _context.Albums.FindAsync(id);
        if (album == null)
        {
            new NotFoundException($"El album con el ID {id} no fue encontrado.");
        }
        
        
        album.AddSong(song);

        await _context.SaveChangesAsync();


        return album;



    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var album = await _context.Albums.FindAsync(id);
        if (album != null)
        {
            _context.Albums.Remove(album);
        }
        await _context.SaveChangesAsync(cancellationToken);

    }

    public async Task<Album> Create(Album album, CancellationToken cancellationToken)
    {
        await _context.Albums.AddAsync(album, cancellationToken);
        await _context.SaveChangesAsync();
        return album;

    }



    public async Task<Album> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Albums.
            Include(a => a.Songs)
        .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        //     if(album == null)
        //     {
        //         throw new KeyNotFoundException($"El album con el ID {id} no fue encontrado.");
        //     }
        //    return album;
    }

}