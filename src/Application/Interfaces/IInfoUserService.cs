namespace Application.Interfaces;

using Domain.Entities;
using Application.DTOs.InfoUser.Request;
using Application.DTOs.InfoUser.Response;


public interface IInfoUserService
{
    Task<GetByIdResponse> GetById(Guid Id);

    Task<CreateResponse> Create(CreateRequest infouserDto, CancellationToken cancellationToken);

    Task<UpdateResponse> Update(Guid Id, UpdateRequest infouserDto);

    Task Delete(Guid Id);
}