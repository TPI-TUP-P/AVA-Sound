namespace Application.Interfaces;

using Domain.Entities;

public interface IInfoUserService
{
    Task<InfoUser> GetById(Guid Id);

    Task Create(InfoUser infouser);

    Task Update(InfoUser infouser);

    Task Delete(Guid Id);
}