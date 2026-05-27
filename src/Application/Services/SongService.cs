using Application.DTOs.Song.Request;
using Application.DTOs.Song.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;


namespace Application.Services;

public class SongService : ISongService
{
    private ISongRepository _song;
    private IUserRepository _user;

    public SongService(ISongRepository song, IUserRepository user)
    {
        _song = song;
        _user= user;
    }

    public async Task<GetByIdResponse> GetById(Guid Id, CancellationToken cancellationToken)
    {
       

        var song = await _song.GetById(Id, cancellationToken);

        if (song == null)
            throw new NotFoundException("Song");

        return new GetByIdResponse
        {
            IdArtist = song.IdArtist,
            IdAlbum = song.IdAlbum,
            Title = song.Title,
            Gender = song.Gender,
            Duration = song.Duration,
            AudioBig = song.AudioBig,
            DateUpload = song.DateUpload,
            Views = song.Views
        };
    }


    public async Task<PagerSongResponse<GetByIdResponse>> GetAll(PagerRequest pagerRequest, CancellationToken cancellationToken)
    {
        var songs = await _song.GetAll(pagerRequest.Page,pagerRequest.PageSize, cancellationToken);
        var page = pagerRequest.Page;
        var pageSize = pagerRequest.PageSize;
        var songTotal= await _song.Count();
        var response= new PagerSongResponse<GetByIdResponse>
        {
            Songs = songs.Select(s => new GetByIdResponse
        {
            IdArtist = s.IdArtist,
            IdAlbum = s.IdAlbum ?? Guid.Empty,
            Title = s.Title,
            Gender = s.Gender,
            Duration = s.Duration,
            AudioBig = s.AudioBig,
            DateUpload = s.DateUpload,
            Views = s.Views
        }).ToList(),

        Page=page,
        PageSize=pageSize,
        SongTotal=songTotal,
        PageTotal=(int) Math.Ceiling(songTotal/(double)pageSize)
        };
        return response;
        
    }

    public async Task<CreateResponse> Create(CreateRequest songDto, Guid idUser,CancellationToken cancellationToken)
    {
        if (songDto == null)
        {
            throw new Exception("Datos inválidos");
        }

        var idAlbum = songDto.IdAlbum;

        if (idUser == Guid.Empty)
            throw new FieldEmptyExcepction("idUser");

        var user = await _user.GetById(idUser, cancellationToken);

        if(user == null)
            throw new NotFoundException("User");

        if (string.IsNullOrWhiteSpace(songDto.Title))
            throw new FieldEmptyExcepction("Title");

        if (string.IsNullOrWhiteSpace(songDto.Gender))
            throw new FieldEmptyExcepction("Gender");


        if (string.IsNullOrWhiteSpace(songDto.Duration))
            throw new FieldEmptyExcepction("Duration");

        if (string.IsNullOrWhiteSpace(songDto.AudioBig))
            throw new FieldEmptyExcepction("AudioBig");

        var song = new Song(
            idUser,
            idAlbum,
            songDto.Title,
            songDto.Gender,
            songDto.Duration,
            songDto.AudioBig
        );

        await _song.Create(song, cancellationToken);

        return new CreateResponse
        {
            IdArtist = song.IdArtist,
            IdAlbum = song.IdAlbum,
            Title = song.Title,
            Gender = song.Gender,
            Duration = song.Duration,
            AudioBig = song.AudioBig,
            DateUpload = song.DateUpload,
            Views = song.Views
        };
    }

    public async Task<UpdateResponse> Update(Guid id, UpdateRequest songDto, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            throw new Exception("Id inválido");

        if (songDto == null)
            throw new Exception("Datos inválidos");

        var song = await _song.GetById(id, cancellationToken);

        if (song == null)
            throw new Exception("La canción no existe");

        if (string.IsNullOrWhiteSpace(songDto.Title))
            throw new FieldEmptyExcepction("Title");

        if (string.IsNullOrWhiteSpace(songDto.Gender))
            throw new FieldEmptyExcepction("Gender");

        if (string.IsNullOrWhiteSpace(songDto.Duration))
            throw new FieldEmptyExcepction("Duration");

        if (string.IsNullOrWhiteSpace(songDto.AudioBig))
            throw new FieldEmptyExcepction("AudioBig");


        song.UpdateInfo(
            songDto.Title,
            songDto.Gender,
            songDto.Duration,
            songDto.AudioBig
        );

        await _song.Update(song, cancellationToken);

        return new UpdateResponse
        {
            Title = song.Title,
            Gender = song.Gender,
            Duration = song.Duration,
            AudioBig = song.AudioBig
        };
    }


    public async Task Delete(Guid Id, CancellationToken cancellationToken)
    {
        if (Id == Guid.Empty)
            throw new FieldEmptyExcepction("Id");

        var song = await _song.GetById(Id, cancellationToken);

        if (song == null)
            throw new Exception("la cancion no existe");

        await _song.Delete(Id, cancellationToken);
    }


}