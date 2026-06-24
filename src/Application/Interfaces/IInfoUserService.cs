namespace Application.Interfaces;

using Domain.Entities;
using Application.DTOs.InfoUser.Request;
using Application.DTOs.InfoUser.Response;


public interface IInfoUserService
{
    Task<GetByIdResponse> GetById(Guid Id, CancellationToken cancellationToken);

    Task<CreateResponse> Create(CreateRequest infouserDto, Guid IdUser, CancellationToken cancellationToken);

    Task<UpdateResponse> Update(Guid Id, Guid IdUser, UpdateRequest infouserDto, CancellationToken cancellationToken);

    Task Delete(Guid Id, Guid IdUser, CancellationToken cancellationToken);
}