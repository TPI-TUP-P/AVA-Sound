namespace Application.Interfaces;

using Application.DTOs.Album.Request;
using Application.DTOs.Album.Response;
using Domain.Entities;

public interface IAlbumService
{
    Task<GetByIdResponse> GetById(Guid id);
    Task<List<GetAllResponse>> GetAll();
    Task<CreateResponse> Create(CreateRequest albumDto, CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid Id, UpdateRequest albumDto);
    Task Delete(Guid id);
}


