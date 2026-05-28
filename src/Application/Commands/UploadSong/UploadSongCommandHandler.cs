// Application/Songs/Commands/UploadSong/UploadSongCommandHandler.cs

using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Interfaces;

public class UploadSongCommandHandler
{
    private readonly IStorageService _storageService;
    private readonly ISongRepository _songRepository;

    public UploadSongCommandHandler(IStorageService storageService, ISongRepository songRepository)
    {
        _storageService = storageService;
        _songRepository = songRepository;
    }

    public async Task<Guid> Handle(UploadSongCommand command)
    {
        // 1. Sube el archivo → te devuelve "uuid.mp3"
       var audioBig = await _storageService.UploadSong(
    command.AudioStream,
    command.FileName,
    command.ContentType
);

        // 2. Crea la entidad con el path
        var song = new Song(
            command.IdArtist,
            command.IdAlbum,
            command.Title,
            command.Gender,
            command.Duration,
            audioBig  // ← "uuid.mp3"
        );

        // 3. Persiste en DB
        await _songRepository.Add(song);

        return song.Id;
    }
}