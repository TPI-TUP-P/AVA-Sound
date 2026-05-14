using Application.DTOs.Song.Request;
using Application.DTOs.Song.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;


namespace Application.Services;

public class SongService : ISongService
{
    private ISongRepository _song;
    public SongService ( ISongRepository song)
    {
        _song=song;
    }

    public async Task<GetByIdResponse> GetById(Guid Id)
    {
        if (Id==Guid.Empty)
            throw new Exception("el id no existe");

        var song = await _song.GetById(Id);

        if(song == null)
            throw new Exception("la cancion no existe");

        return new GetByIdResponse
        {
            IdArtist=song.IdArtist,
            IdAlbum=song.IdAlbum,
            Title=song.Title,
            Gender=song.Gender,
            Duration=song.Duration,
            AudioBig=song.AudioBig,
            DateUpload=song.DateUpload,
            Views=song.Views
        };
    }


    public async Task<List<GetAllResponse>> GetAll()
{
    var songs = await _song.GetAll();

    return songs.Select(s => new GetAllResponse
    {
        IdArtist=s.IdArtist,
        IdAlbum=s.IdAlbum ?? Guid.Empty,
        Title=s.Title,
        Gender=s.Gender,
        Duration=s.Duration,
        AudioBig=s.AudioBig,
        DateUpload=s.DateUpload,
        Views=s.Views
    }).ToList();
}

public async Task<CreateResponse> Create(CreateRequest songDto)
{
    if (songDto == null)
    {
        throw new Exception("Datos inválidos");
    }
        var idAlbum = songDto.IdAlbum;


        if (songDto.IdArtist == Guid.Empty)
            throw new Exception("IdArtist inválido");


        if (string.IsNullOrWhiteSpace(songDto.Title))
            throw new Exception("Title es obligatorio");

        if (string.IsNullOrWhiteSpace(songDto.Gender))
            throw new Exception("Gender es obligatoria");

        if (string.IsNullOrWhiteSpace(songDto.Duration))
            throw new Exception("Duration es obligatoria");

        if (string.IsNullOrWhiteSpace(songDto.AudioBig))
            throw new Exception("AudioBig es obligatoria");

        

    var song = new Song(
        songDto.IdArtist,
        idAlbum,
        songDto.Title,
        songDto.Gender,
        songDto.Duration,
        songDto.AudioBig
    );

    await _song.Create(song);

    return new CreateResponse
    {
        IdArtist=song.IdArtist,
        IdAlbum=song.IdAlbum,
        Title=song.Title,
        Gender=song.Gender,
        Duration=song.Duration,
        AudioBig=song.AudioBig,
        DateUpload=song.DateUpload,
        Views=song.Views
    };
}

public async Task<UpdateResponse> Update(Guid id, UpdateRequest songDto)
{
    if (id == Guid.Empty)
        throw new Exception("Id inválido");

    if (songDto == null)
        throw new Exception("Datos inválidos");

    var song = await _song.GetById(id);

    if (song == null)
        throw new Exception("La canción no existe");

    if (string.IsNullOrWhiteSpace(songDto.Title))
            throw new Exception("Title es obligatorio");

    if (string.IsNullOrWhiteSpace(songDto.Gender))
            throw new Exception("Gender es obligatoria");

    if (string.IsNullOrWhiteSpace(songDto.Duration))
            throw new Exception("Duration es obligatoria");

    if (string.IsNullOrWhiteSpace(songDto.AudioBig))
            throw new Exception("AudioBig es obligatoria");

    song.UpdateInfo(
        songDto.Title,
        songDto.Gender,
        songDto.Duration,
        songDto.AudioBig
    );

    await _song.Update(song);

    return new UpdateResponse
    {
        Title = song.Title,
        Gender = song.Gender,
        Duration = song.Duration,
        AudioBig = song.AudioBig
    };
}


public async Task Delete(Guid Id)
    {
        if (Id==Guid.Empty)
            throw new Exception("el id no existe");

        var song=await _song.GetById(Id);

        if(song==null)
            throw new Exception("la cancion no existe");

        await _song.Delete(Id);
    }


}