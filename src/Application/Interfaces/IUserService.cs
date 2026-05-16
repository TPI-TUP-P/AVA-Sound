
using Application.DTOs.User.request;
using Application.DTOs.User.response;

namespace Application.Interfaces;

public interface IUserService
{
    Task<GetByIdResponse> GetById(Guid id, CancellationToken cancellationToken);
    Task<List<GetAllResponse>> GetAll(CancellationToken cancellationToken);
    Task<CreateResponse> Create(CreateRequest UserDto, CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid Id, UpdateRequest UsermDto, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
}