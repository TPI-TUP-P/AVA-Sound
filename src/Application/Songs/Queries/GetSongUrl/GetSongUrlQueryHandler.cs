// Application/Songs/Queries/GetSongUrl/GetSongUrlQueryHandler.cs
using Domain.Interfaces;
using Infrastructure.Interfaces;

public class GetSongUrlQueryHandler
{
    private readonly ISongRepository _songRepository;
    private readonly IStorageService _storageService;

    public GetSongUrlQueryHandler(ISongRepository songRepository, IStorageService storageService)
    {
        _songRepository = songRepository;
        _storageService = storageService;
    }

    public async Task<string> Handle(GetSongUrlQuery query)
    {
        var song = await _songRepository.GetById(query.SongId, CancellationToken.None);
        if (song is null) throw new Exception("Song not found");

        // AudioBig tiene "uuid.mp3", genera la URL firmada
        return await _storageService.GetSongUrl(song.AudioBig);
    }
}