using Application.DTOs.Album.Request;
using Application.DTOs.Album.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;


namespace Application.Services;

public class AlbumService : IAlbumService
{
    private IAlbumRepository _album;
    private IUserRepository _user;
    private readonly ISongRepository _song;


    public AlbumService(IAlbumRepository album, ISongRepository song, IUserRepository user)
    {
        _album = album;
        _song = song;
        _user = user;
    }


    public async Task<GetByIdResponse> GetById(Guid Id, CancellationToken cancellationToken)
    {
    
        var album = await _album.GetById(Id, cancellationToken);

        if (album == null)
        {
            throw new NotFoundException("Album");
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
            album.IdArtist,
            album.Title,
            album.ReleasteDate,
            album.FrontPage
        )).ToList();
    }

    public async Task<CreateResponse> Create(CreateRequest albumDto, Guid idUser,CancellationToken cancellationToken)
    {

        var user = await _user.GetById(idUser, cancellationToken);

        if(user.IsArtist is false)
        {
            throw new UserIsNotArtistException("The user is not an artist");
        }

        var albumData = new Album(
            user.Id,
            albumDto.Title,
            albumDto.ReleaseDate,
            albumDto.FrontPage,
            albumDto.Description

        );

        var albumCreated = await _album.Create(albumData, cancellationToken);


        return new CreateResponse(
            albumCreated.Id,
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
        if (existingAlbum is null)
        {
            throw new NotFoundException("Album");
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
            throw new Exception("The ID is empty");
        }

        return _album.Delete(Id, cancellationToken);
    }



    public async Task<GetByIdResponse> AddSong(Guid id, Guid idSong, CancellationToken cancellationToken)
    {
        var album = await _album.GetById(id, cancellationToken);
        var song = await _song.GetById(idSong, cancellationToken);

        if(album is null)
        {
            throw new NotFoundException("Album");
        }

        if(song is null)
        {
            throw new NotFoundException("Song");
        }

        album.AddSong(song);
        await _album.Update(album, cancellationToken);

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

  
}