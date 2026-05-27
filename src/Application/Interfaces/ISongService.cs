namespace Application.Interfaces;

using Application.DTOs.Song.Response;
using Application.DTOs.Song.Request;
using Domain.Entities;

public interface ISongService
{
    Task<GetByIdResponse> GetById(Guid id, CancellationToken cancellationToken);
    
    Task<PagerSongResponse<GetByIdResponse>> GetAll(PagerRequest pagerRequest, CancellationToken cancellationToken);
    Task<CreateResponse> Create(CreateRequest songDto, Guid idUser,CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid Id, UpdateRequest songDto, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
}