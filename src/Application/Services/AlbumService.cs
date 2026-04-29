using Application.DTOs.Album;
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

    public Task Create(CreateAlbumDto albumDto)
    {
        if (albumDto == null)
        {
            throw new Exception("El album esta vacio");
        }
        return _album.Create(new Album(
            albumDto.IdArtist,
            albumDto.Title,
            albumDto.ReleaseDate,
            albumDto.FrontPage,
            albumDto.Description
        ));
        


    }


    public Task Update(UpdateAlbumDto albumDto)
    {
        return _album.Update(new Album (
            albumDto.IdArtist,
            albumDto.Title,
            albumDto.ReleaseDate,
            albumDto.FrontPage,
            albumDto.Description
            
        ));
    }

    public Task Delete(Guid Id)
    {
        return _album.Delete(Id);
    }
}