namespace Application.Interfaces;

using Application.DTOs.Review.Request;
using Application.DTOs.Review.Response;


public interface IReviewService
{
    Task<List<GetBySongResponse>> GetBySong(Guid Id);
    Task<CreateResponse> Create(CreateRequest reviewDto, CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid Id, UpdateRequest reviewDto);
    Task Delete(Guid id);
}