
using Application.DTOs.User.request;
using Application.DTOs.User.Request;
using Application.DTOs.User.response;
using Application.DTOs.User.Response;

namespace Application.Interfaces;

public interface IUserService
{
    Task<GetByIdResponse> GetById(Guid id, CancellationToken cancellationToken);
    Task<PagerResponse<GetAllResponse>> GetAll( CancellationToken cancellationToken);
    // Task<List<GetAllResponse>> GetAll(CancellationToken cancellationToken);
    Task<CreateResponse> Create(CreateRequest UserDto, CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid Id, UpdateRequest UsermDto,Guid userId, CancellationToken cancellationToken);
    Task Delete(Guid id, Guid userId, CancellationToken cancellationToken);

    Task HandleAdmin(Guid userId, Guid currentUserId, CancellationToken cancellationToken);
    
    Task<List<GetAllArtistResponse>> GetAllArtists(CancellationToken cancellationToken);

}