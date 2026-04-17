using Domain.Entities;

namespace Domain.Interfaces;

public interface IInfoUser
{
    Task<IInfoUser?> GeyById(Guid idInfoUser);

    Task Add(InfoUser infoUser);

    Task Update(InfoUser infoUser);

    Task Delete(Guid idInfoUser);
}