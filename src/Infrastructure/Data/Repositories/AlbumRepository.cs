namespace Infrastructure.Data.Repositories;
using Domain.Entities;
using Domain.Interfaces;

public class AlbumRepository : IAlbumRepository
{
    private static List<Album> _albums = new();
    public Task<List<Album>> GetAll()
    {
        return Task.FromResult(_albums);
    }


    public void  Update(Album album)
    {
        var existingAlbum = _albums.FirstOrDefault(a=> a.Id == album.Id);
        if (existingAlbum != null)
        {
            _albums.Remove(existingAlbum);
        }
        else
        {
            _albums.Add(album);
        }
    }


    public Task AddSong(Guid id)
    {
        return Task.CompletedTask;
    }

    public Task Delete(Guid id)
    {
        var album = _albums.FirstOrDefault(a => a.Id == id);
        if (album != null)
        {
            _albums.Remove(album);
        }
        return Task.CompletedTask;
    }

    async Task IRepository<Album>.Update(Album album)
    {
        Update(album);
        await Task.CompletedTask;
    }
    

    public Task Create(Album album)
    {
        _albums.Add(album);
        return Task.CompletedTask; 
    }



    public Task<Album> GetById(Guid id)
    {
        var album = _albums.FirstOrDefault(a=> a.Id == id);
        if (album == null)
        {
            throw new Exception("Album not found");
        }
        return Task.FromResult(album);

    }

    
}