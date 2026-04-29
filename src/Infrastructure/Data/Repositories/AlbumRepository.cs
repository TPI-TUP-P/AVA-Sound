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
    public async Task Update(Album album)
{
    var existing = await _context.Albums.FindAsync(album.Id);

    if (existing == null)
        return;

    existing.Title = album.Title;
    existing.Description = album.Description;
    existing.ReleasteDate = album.ReleasteDate;
    existing.FrontPage = album.FrontPage;

    await _context.SaveChangesAsync();
}

    public async Task AddSong(Guid id)
    {   
        
    }

    public async Task Delete(Guid id)
    {
        var album =await _context.Albums.FindAsync(id);
        if (album != null)
        {
            _context.Albums.Remove(album);
        }
        await _context.SaveChangesAsync();
        
    }

    public async Task Create(Album album)
    {
        var albumCreated = await _context.Albums.AddAsync(album);
        await _context.SaveChangesAsync();
                
    }



    public async Task<Album> GetById(Guid id)
    {
        var album =await _context.Albums.FindAsync(id);
        if(album == null)
        {
            throw new KeyNotFoundException($"Album with ID {id} not found.");
        }
       return album;
    }

    
}