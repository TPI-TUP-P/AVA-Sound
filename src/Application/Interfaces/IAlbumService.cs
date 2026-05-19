namespace Application.Interfaces;

using Application.DTOs.Album.Request;
using Application.DTOs.Album.Response;
using Domain.Entities;

public interface IAlbumService
{
    Task<GetByIdResponse> GetById(Guid id, CancellationToken cancellationToken);
    Task<List<GetAllResponse>> GetAll();
    Task<CreateResponse> Create(CreateRequest albumDto,Guid idUser,CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid Id, UpdateRequest albumDto, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);

    Task<GetByIdResponse> AddSong(Guid id, Guid idSong, CancellationToken cancellationToken);
}


