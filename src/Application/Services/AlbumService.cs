using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public  class AlbumService : IAlbumService
{
    private IAlbumRepository _album;
    public AlbumService(IAlbumRepository album)
    {
        _album = album;

    }


    public Task <Album> GetById(Guid id)
    {
    
    
        if(id == Guid.Empty)
        {
            throw new Exception("Id es vacio");
        }

    

        return _album.GetById(id);
    }
    public Task <List<Album>> GetAll()
    {
        return _album.GetAll();
    }

    public Task Create(Album album)
    {   
        if(album == null)
        {
            throw new Exception("El album esta vacio");
        }
        return _album.Create(album);
        
        
    }


    public Task Update(Album album)
    {
        return _album.Update(album);
    }

    public Task Delete(Guid id)
    {
        return _album.Delete(id);
    }
}