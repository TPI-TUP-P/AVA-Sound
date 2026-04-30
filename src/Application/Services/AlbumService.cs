using Application.DTOs.Album.Request;
using Application.DTOs.Album.Response;
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


    public async Task<GetByIdResponse> GetById(Guid Id)
    {
        if (Id == Guid.Empty)
                {
                    throw new Exception("Id es vacio");
                }

        var album = await _album.GetById(Id);

        if(album == null)
        {
            throw new Exception("El album no existe");
        }
        
        return new GetByIdResponse
        {
            Id = album.Id,
            IdArtist = album.IdArtist,
            Title = album.Title,
            ReleasteDate = album.ReleasteDate,
            FrontPage = album.FrontPage,
            Description = album.Description
        };

    }
    public async Task<List<GetAllResponse>> GetAll()
    {
        var albums =await _album.GetAll();
        return albums.Select(album => new GetAllResponse
        {
            Id = album.Id,
            Title = album.Title,
            ReleasteDate = album.ReleasteDate,
            FrontPage = album.FrontPage,
        }).ToList();
    }

    public async Task<CreateResponse> Create(CreateRequest albumDto)
    {
        if (albumDto == null)
        {
            throw new Exception("El album esta vacio");
        }
        var albumCreated=await  _album.Create(new Album(
            albumDto.IdArtist,
            albumDto.Title,
            albumDto.ReleaseDate,
            albumDto.FrontPage,
            albumDto.Description
        ));


        return new CreateResponse
        {
            Title = albumCreated.Title,
            ReleasteDate = albumCreated.ReleasteDate,
            FrontPage = albumCreated.FrontPage,
            Description = albumCreated.Description

        };
        



    }


    public async Task<UpdateResponse> Update(Guid Id, UpdateRequest albumDto)
    {
        var existingAlbum = await _album.GetById(Id);
        if (existingAlbum == null)
        {
            throw new Exception("El album no existe");
        }
        
        if (albumDto == null)
        {
            throw new Exception("El album esta vacio");
        }

          existingAlbum.Title = albumDto.Title;
          existingAlbum.ReleasteDate=  albumDto.ReleaseDate;
           existingAlbum.FrontPage= albumDto.FrontPage;
           existingAlbum.Description= albumDto.Description;



        await _album.Update(
            existingAlbum
        );

        return new UpdateResponse
        {
            Id = existingAlbum.Id,
            IdArtist = existingAlbum.IdArtist,
            Title = existingAlbum.Title,
            ReleasteDate = existingAlbum.ReleasteDate,
            FrontPage = existingAlbum.FrontPage,
            Description = existingAlbum.Description
            

        };
    }

    public Task Delete(Guid Id)
    {

        if(Id == Guid.Empty)
        {
            throw new Exception("Id es vacio");
        }
        
        return _album.Delete(Id);
    }
}