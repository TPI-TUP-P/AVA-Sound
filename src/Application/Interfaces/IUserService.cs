
using Application.DTOs.User.request;
using Application.DTOs.User.response;

namespace Application.Interfaces;

public interface IUserService
{
    Task<GetByIdResponse> GetById(Guid id);
    
    Task<CreateResponse> Create(CreateRequest UserDto, CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid Id, UpdateRequest UsermDto);
    Task Delete(Guid id);
    Task<PagerResponse<GetByIdResponse>> GetAll(PagerRequest pagerRequest);
}