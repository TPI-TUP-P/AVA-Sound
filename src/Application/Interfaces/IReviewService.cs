namespace Application.Interfaces;

using Application.DTOs.Review.Request;
using Application.DTOs.Review.Response;


public interface IReviewService
{
    Task<List<GetBySongResponse>> GetBySong(Guid Id, CancellationToken cancellationToken);
    Task<GetByIdResponse> GetById(Guid Id, CancellationToken cancellationToken);
    Task<CreateResponse> Create(CreateRequest reviewDto, CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid Id, Guid idUserToken, UpdateRequest reviewDto, CancellationToken cancellationToken);
    Task Delete(Guid id, Guid idUserToken, CancellationToken cancellationToken);
}