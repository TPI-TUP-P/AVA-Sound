namespace Application.Interfaces;

using Application.DTOs.Song.Response;
using Application.DTOs.Song.Request;
using Domain.Entities;


public interface ISongService
{
    Task<GetByIdResponse> GetById(Guid id, CancellationToken cancellationToken);
    // Task <GetByAlbumResponse> GetByAlbum(Guid id);
    // Task <GetByUserResponse> GetByUser(Guid id);
    Task<List<GetAllResponse>> GetAll();
    Task<CreateResponse> Create(CreateRequest songDto, CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid Id, UpdateRequest songDto, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
}