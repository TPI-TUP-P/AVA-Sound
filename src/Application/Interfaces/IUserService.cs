
using Application.DTOs.User.request;
using Application.DTOs.User.Request;
using Application.DTOs.User.response;
using Application.DTOs.User.Response;

namespace Application.Interfaces;

public interface IUserService
{
    Task<GetByIdResponse> GetById(Guid id, CancellationToken cancellationToken);
    Task<PagerResponse<GetByIdResponse>> GetAll(PagerRequest pagerRequest, CancellationToken cancellationToken);
    // Task<List<GetAllResponse>> GetAll(CancellationToken cancellationToken);
    Task<CreateResponse> Create(CreateRequest UserDto, CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid Id, UpdateRequest UsermDto, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
}