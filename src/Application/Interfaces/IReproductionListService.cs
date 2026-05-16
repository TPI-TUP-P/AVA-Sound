using Application.DTOs.ReproductionList.Request;
using Application.DTOs.ReproductionList.Response;
using Domain.Entities;

namespace Application.Interfaces;

public interface IReproductionListService
{
    Task<GetByIdResponse> GetById(Guid id);
    Task<CreateResponse> Create(CreateRequest reproductionsListDto, CancellationToken cancellationToken);
    Task AddSong(Guid listId, Guid songId, CancellationToken cancellationToken);
    Task RemoveSong(Guid listId, Guid songId, CancellationToken cancellationToken);
    Task Delete(Guid id);
}