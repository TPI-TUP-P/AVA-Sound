using Application.DTOs.Album.Request;
using Application.DTOs.Album.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;


namespace Application.Services;

public class AlbumService : IAlbumService
{
    private IAlbumRepository _album;
    private readonly ISongRepository _song;


    public AlbumService(IAlbumRepository album, ISongRepository song)
    {
        _album = album;
        _song = song;
    }


    public async Task<GetByIdResponse> GetById(Guid Id, CancellationToken cancellationToken)
    {
        if (Id == Guid.Empty)
        {
            throw new Exception("Id es vacio");
        }

        var album = await _album.GetById(Id, cancellationToken);

        if (album == null)
        {
            throw new Exception("El album no existe");
        }

        return new GetByIdResponse
        (
           album.Id,
            album.IdArtist,
            album.Title,
            album.ReleasteDate,
             album.FrontPage,
            album.Description
            ,
            album.Songs.ToList()

        );

    }
    public async Task<List<GetAllResponse>> GetAll()
    {
        var albums = await _album.GetAll();
        return albums.Select(album => new GetAllResponse
        (
            album.Id,
            album.Title,
            album.ReleasteDate,
            album.FrontPage
        )).ToList();
    }

    public async Task<CreateResponse> Create(CreateRequest albumDto, CancellationToken cancellationToken)
    {
        if (albumDto == null)
        {
            throw new Exception("El album esta vacio");
        }

        var albumData = new Album(
            albumDto.IdArtist,
            albumDto.Title,
            albumDto.ReleaseDate,
            albumDto.FrontPage,
            albumDto.Description

        );

        var albumCreated = await _album.Create(albumData, cancellationToken);


        return new CreateResponse(

            albumCreated.IdArtist,
            albumCreated.Title,
            albumCreated.ReleasteDate,
            albumCreated.FrontPage,
            albumCreated.Description
        );



    }


    public async Task<UpdateResponse> Update(Guid Id, UpdateRequest albumDto, CancellationToken cancellationToken)
    {
        var existingAlbum = await _album.GetById(Id, cancellationToken);
        if (existingAlbum == null)
        {
            throw new Exception("El album no existe");
        }

        if (albumDto == null)
        {
            throw new Exception("El album esta vacio");
        }

        if (albumDto.Title != null)
        {

            existingAlbum.Title = albumDto.Title;
        }

        if (albumDto.ReleaseDate != default)
        {

            existingAlbum.ReleasteDate = albumDto.ReleaseDate;
        }
        if (albumDto.FrontPage != null)
        {
            existingAlbum.FrontPage = albumDto.FrontPage;
        }
        if (albumDto.Description != null)
        {
            existingAlbum.Description = albumDto.Description;
        }


        await _album.Update(
            existingAlbum, cancellationToken
        );

        return new UpdateResponse
        (

            existingAlbum.Id,
            existingAlbum.IdArtist,
            existingAlbum.Title,
            existingAlbum.ReleasteDate,
            existingAlbum.FrontPage,
            existingAlbum.Description


        );
    }

    public Task Delete(Guid Id, CancellationToken cancellationToken)
    {

        if (Id == Guid.Empty)
        {
            throw new Exception("Id es vacio");
        }

        return _album.Delete(Id, cancellationToken);
    }



    public async Task<GetByIdResponse> AddSong(Guid id, Guid idSong, CancellationToken cancellationToken)
    {
        var album = await _album.GetById(id, cancellationToken);
        var song = await _song.GetById(idSong, cancellationToken);



        album.AddSong(song);
        await _album.Update(album, cancellationToken);



        // var songs = await _song.GetById(idSong);


        return new GetByIdResponse
        (
            album.Id,
            album.IdArtist,
            album.Title,
            album.ReleasteDate,
            album.FrontPage,
            album.Description,
            album.Songs.ToList()

        );



    }

    // Este metodo no se puede completar porque falta el ABM de song 

    // public Task AddSong(Guid id, Song song)
    // {
    //     var album = _album.GetById(id);

    //     _album.AddSong(song);


    // }
}