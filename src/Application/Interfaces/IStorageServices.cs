namespace Infrastructure.Interfaces;


public interface IStorageService
{
    Task<string> UploadSong(Stream stream, string fileName, string contentType);
    Task<string> GetSongUrl(string nombreArchivo);
}