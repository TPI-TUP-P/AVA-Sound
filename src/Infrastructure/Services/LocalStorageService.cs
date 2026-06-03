using Infrastructure.Data.Services;
using Infrastructure.Interfaces;

public class LocalStorageService : IStorageService
{

    private readonly string _basePath = Path.Combine("tmp", "songs");

    public async Task<string> UploadSong( Stream stream,string fileName,string contentType)
    {
        var filePath = Path.Combine(_basePath, fileName);
        using var fileStream = File.Create(filePath);
        await stream.CopyToAsync(fileStream);
        return filePath;
    }

    public async Task<string> GetSongUrl(string filePath)
    {
        return filePath;
    

    }
}