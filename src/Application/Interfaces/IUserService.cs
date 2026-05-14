
using Application.DTOs.User.request;
using Application.DTOs.User.response;

namespace Application.Interfaces;

public interface IUserService
{
    Task<GetByIdResponse> GetById(Guid id);
    Task<List<GetAllResponse>> GetAll();
    Task<CreateResponse> Create(CreateRequest UserDto, CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid Id, UpdateRequest UsermDto);
    Task Delete(Guid id);
}