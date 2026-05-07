using Application.DTOs.ReproductionList.Request;
using Application.DTOs.ReproductionList.Response;
using Domain.Entities;

namespace Application.Interfaces;
public interface IReproductionListService
{
    Task<GetByIdResponse> GetById(Guid id);
    Task<CreateResponse> Create(CreateRequest reproductionsListDto);
    Task AddSong(Guid listId, Guid songId);
    Task RemoveSong(Guid listId, Guid songId);
    Task Delete(Guid id);
}