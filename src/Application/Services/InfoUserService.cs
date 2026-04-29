using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class InfoUserService : IInfoUserService
{
    private IInfoUserRepository _InfoUser;

    public InfoUserService(IInfoUserRepository infouser)
    {
        _InfoUser = infouser;
    }

    public Task<InfoUser> GetById(Guid Id)
    {
        if (Id == Guid.Empty)
        {
            throw new Exception("id cannot be null");
        }

        return _InfoUser.GetById(Id);
    }

    public Task Create(InfoUser infouser)
    {
        if (infouser == null)
        {
            throw new Exception("empty information");
        }

        return _InfoUser.Create(infouser);
    }

    public Task Update(InfoUser infouser)
    {
        return _InfoUser.Update(infouser);
    }

    public Task Delete(Guid Id)
    {
        if (Id == Guid.Empty)
        {
            throw new Exception("empty id.");
        }
        return _InfoUser.Delete(Id);
    }

}