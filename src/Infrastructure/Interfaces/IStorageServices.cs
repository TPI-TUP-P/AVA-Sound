namespace Infrastructure.Interfaces;

using Infrastructure.DTOs.Storage.Request;
using Infrastructure.DTOs.Storage.Response;
public interface IStorageService
{
    Task<CreateResponse> UploadSong(CreateRequest song);
}