namespace Application.Interfaces;

using Domain.Entities;
using Application.DTOs.Album.Request;
using Application.DTOs.Album.Response;

public interface IInfoUserService
{
    Task<InfoUser> GetById(Guid Id);

    Task Create(InfoUser infouser);

    Task Update(InfoUser infouser);

    Task Delete(Guid Id);
}