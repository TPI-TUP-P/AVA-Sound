using Application.DTOs.Album.Request;
using Application.DTOs.Album.Response;
using Application.Interfaces;

using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;


namespace Application.Services;

public class AlbumService : IAlbumService
{
    private IAlbumRepository _album;
    private IUserService _user;
    private readonly ISongRepository _song;


    public AlbumService(IAlbumRepository album, ISongRepository song, IUserService user)
    {
        _album = album;
        _song = song;
        _user = user;
    }


    public async Task<GetByIdResponse> GetById(Guid id, CancellationToken cancellationToken)
    {
    
        var album = await _album.GetById(id, cancellationToken);

    

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


    public async Task<List<GetAllResponse>> GetAllByArtist(Guid idArtist, CancellationToken cancellationToken)
    {

        var user = await _user.GetById(idArtist, cancellationToken);

        if(user.IsArtist is false)
        {
            throw new NotFoundException("User");
        }

        var albums = await _album.GetAllByArtist(idArtist, cancellationToken);
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
            albumDto.Title!,
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

    public async Task<UpdateResponse> Update(Guid id,Guid idUser, UpdateRequest albumDto, CancellationToken cancellationToken)
    {
        var existingAlbum = await _album.GetById(id, cancellationToken);
        var user = await _user.GetById(existingAlbum.IdArtist, cancellationToken);
        
        if (existingAlbum is null)
        {
            throw new NotFoundException("Album");
        }

        if(existingAlbum.IdArtist != idUser)
        {
            throw new ForbiddenException("Album");
        }

        if(user.IsArtist is false)
        {
            throw new UserIsNotArtistException("The user is not an artist");
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

    public async Task Delete(Guid id, Guid idUser,CancellationToken cancellationToken)
    {
        
        var album = await _album.GetById(id, cancellationToken);
        var user = await _user.GetById(idUser, cancellationToken);


        if(album is null)
        {
            throw new NotFoundException("Album");
        }

        if (idUser !=  album.IdArtist && user.Role != "Admin")
        {
            throw new ForbiddenException();

        }

        if(user.IsArtist is false)
        {
            throw new UserIsNotArtistException("The user is not an artist");
        }

        foreach(var song in album.Songs)
        {
            song.RemoveFromAlbum();
           await _song.Update(song, cancellationToken);
        }
    
         await _album.Delete(id, cancellationToken);



    }



    public async Task<GetByIdResponse> AddSong(Guid id, Guid idSong, Guid idUser,CancellationToken cancellationToken)
    {
        var album = await _album.GetById(id, cancellationToken);
        var song = await _song.GetById(idSong, cancellationToken);
        var user = await _user.GetById(idUser, cancellationToken);

        
        if(album is null)
        {
            throw new NotFoundException("Album");
        }

        if(song is null)
        {
            throw new NotFoundException("Song");
        }

        // if(user.Id != idUser)
        // {
        //     throw new Exception("You don't have permission to add this song");
        // }

        if(user.Id != song.IdArtist)
        {   
            throw new ForbiddenException();
        }

        if(song.IdAlbum != Guid.Empty)
        {   
            throw new FieldEmptyExcepction("IdAlbum");
        }

        if(song.IdAlbum == album.Id)
        {
            throw new AlreadyExistExcepction(song.Title);
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


    public async Task<GetByIdResponse> DeleteSong(Guid id, Guid idSong, Guid idUser,CancellationToken cancellationToken)
    {
        var album = await _album.GetById(id, cancellationToken);
        var song = await _song.GetById(idSong, cancellationToken);
        var user = await _user.GetById(idUser, cancellationToken);

        
        if(album is null)
        {
            throw new NotFoundException("Album");
        }

        if(song is null)
        {
            throw new NotFoundException("Song");
        }

        if(user.Id != idUser && user.Role != "Admin" )
        {
            throw new ForbiddenException();
        }

        if(song.IdAlbum is null)
        {
            throw new SongNotInAlbumException(song.Title);
        }

        
        album.DeleteSong(song);
        song.RemoveFromAlbum();
        await _album.Update(album, cancellationToken);

        return new GetByIdResponse(
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