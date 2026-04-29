using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AlbumService : IAlbumService
{
    private IAlbumRepository _album;
    public AlbumService(IAlbumRepository album)
    {
        _album = album;

    }


    public Task<Album> GetById(Guid Id)
    {


        if (Id == Guid.Empty)
        {
            throw new Exception("Id es vacio");
        }



        return _album.GetById(Id);
    }
    public Task<List<Album>> GetAll()
    {
        return _album.GetAll();
    }

    public Task Create(Album album)
    {
        if (album == null)
        {
            throw new Exception("El album esta vacio");
        }
        return _album.Create(album);


    }


    public Task Update(Album album)
    {
        return _album.Update(album);
    }

    public Task Delete(Guid Id)
    {
        return _album.Delete(Id);
    }
}