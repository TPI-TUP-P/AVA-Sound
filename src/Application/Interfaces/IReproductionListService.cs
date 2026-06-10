using Application.DTOs.ReproductionList.Request;
using Application.DTOs.ReproductionList.Response;
using Domain.Entities;

namespace Application.Interfaces;

public interface IReproductionListService
{
    Task<GetByIdResponse> GetById(Guid id, CancellationToken cancellationToken);
    Task<CreateResponse> Create(CreateRequest reproductionsListDto, CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid id, UpdateRequest updateRequest, Guid idUser, CancellationToken cancellationToken);
    Task AddSong(Guid listId, Guid songId, Guid idUser, CancellationToken cancellationToken);
    Task RemoveSong(Guid listId, Guid songId, Guid idUser, CancellationToken cancellationToken);
    Task Delete(Guid id, Guid idUser, CancellationToken cancellationToken);

    Task<List<GetAllResponse>> GetByIdUser(Guid idUser, CancellationToken cancellationToken);
}

