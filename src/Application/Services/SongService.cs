using Application.DTOs.Song.Request;
using Application.DTOs.Song.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Interfaces;



namespace Application.Services;

public class SongService : ISongService
{
    private ISongRepository _song;
    private IUserRepository _user;
    private IStatisticService  _statistic;

    private IStorageService _storageService;


    public SongService(ISongRepository song, IUserRepository user, IStorageService storage, IStorageService storageService, IStatisticService statistic)
    {
        _song = song;
        _user = user;
        _storageService = storageService;
        _statistic = statistic;
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
        var songs = await _song.GetAll(pagerRequest.Page, pagerRequest.PageSize, cancellationToken);
        var page = pagerRequest.Page;
        var pageSize = pagerRequest.PageSize;
        var songTotal = await _song.Count();
        var response = new PagerSongResponse<GetByIdResponse>
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

            Page = page,
            PageSize = pageSize,
            SongTotal = songTotal,
            PageTotal = (int)Math.Ceiling(songTotal / (double)pageSize)
        };
        return response;

    }
    public async Task<CreateResponse> Create(
        CreateRequest songDto,
        Stream audioStream,
        string fileName,
        string contentType,
        Guid idUser,
        CancellationToken cancellationToken)
    {
        if (songDto == null)
            throw new FieldEmptyExcepction("songDto");

        if (idUser == Guid.Empty)
            throw new FieldEmptyExcepction("idUser");

        var user = await _user.GetById(idUser, cancellationToken);
        if (user == null)
            throw new NotFoundException("User");



        if (string.IsNullOrWhiteSpace(songDto.Title))
            throw new FieldEmptyExcepction("Title");
        if (string.IsNullOrWhiteSpace(songDto.Gender))
            throw new FieldEmptyExcepction("Gender");
        if (string.IsNullOrWhiteSpace(songDto.Duration))
            throw new FieldEmptyExcepction("Duration");


        var audioBig = await _storageService.UploadSong(audioStream, fileName, contentType);


        var song = new Song(
            idUser,
            songDto.IdAlbum == Guid.Empty ? null : songDto.IdAlbum,
            songDto.Title,
            songDto.Gender,
            songDto.Duration,
            audioBig
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
    public async Task<UpdateResponse> Update(Guid id, UpdateRequest songDto, Guid idUser,  CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            throw new FieldEmptyExcepction("Id");

        if (songDto == null)
            throw new FieldEmptyExcepction("Dto");

        var song = await _song.GetById(id, cancellationToken);

        if(idUser!=song.IdArtist)
            throw new IdNotMatchException();


        if (song == null)
            throw new NotFoundException("song");

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


    public async Task Delete(Guid Id, Guid idUser, CancellationToken cancellationToken)
    {
        
        if (Id == Guid.Empty)
            throw new FieldEmptyExcepction("Id");

        var song = await _song.GetById(Id, cancellationToken);

        if(song.Id != idUser)
            throw new IdNotMatchException();

        if (song == null)
            throw new NotFoundException("song");

        await _song.Delete(Id, cancellationToken);
    }


    public async Task<string> GetSongUrl(Guid songId,Guid IdUser, CancellationToken cancellationToken)
    {
        if (songId == Guid.Empty)
            throw new FieldEmptyExcepction("songId");

        var song = await _song.GetById(songId, cancellationToken);
        if (song is null)
            throw new NotFoundException("Song");

        if(song.IdArtist != IdUser)
        {
        song.AddView();
            
        await _statistic.RegisterReproductionAsync(songId, IdUser,cancellationToken);
        await _song.Update(song, cancellationToken);
        return await _storageService.GetSongUrl(song.AudioBig);

        } else
        {
        return await _storageService.GetSongUrl(song.AudioBig);
            
        }


    }

}