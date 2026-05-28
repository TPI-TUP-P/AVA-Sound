namespace Infrastructure.Interfaces;

using Infrastructure.DTOs.Storage.Request;
using Infrastructure.DTOs.Storage.Response;
public interface IStorageService
{
    Task<string> UploadSong(Stream stream, string fileName, string contentType);
    Task<string> GetSongUrl(string nombreArchivo);
}