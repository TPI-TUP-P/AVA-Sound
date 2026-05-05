using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Application.DTOs.InfoUser.Request;
using Application.DTOs.InfoUser.Response;

namespace Application.Services;

public class InfoUserService : IInfoUserService
{
    private IInfoUserRepository _InfoUser;

    public InfoUserService(IInfoUserRepository infouser)
    {
        _InfoUser = infouser;
    }

    public async Task<GetByIdResponse> GetById(Guid Id)
    {
        if (Id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty");
        }

        var infouser = await _InfoUser.GetById(Id);

        return new GetByIdResponse
        {
            IdUser = infouser.IdUser,
            ProfilePicture = infouser.ProfilePicture,
            Biography = infouser.Biography,
            Country = infouser.Country
        }
        ;
    }

    public async Task<CreateResponse> Create(CreateRequest infouserDto)
    {
        if (infouserDto == null)
        {
            throw new Exception("empty information.");
        }
        if (infouserDto.ProfilePicture is null)
        {
            throw new Exception("ProfilePicture is null.");
        }
        if (infouserDto.Biography is null || infouserDto.Country is null)
        {
            throw new Exception("empty information.");
        }
        var newInfoUser = new InfoUser(
            infouserDto.IdUser,
            infouserDto.ProfilePicture,
            infouserDto.Biography,
            infouserDto.Country
        );
        var infouserCreated = await _InfoUser.Create(newInfoUser);
        return new CreateResponse
        {
            IdUser = infouserCreated.IdUser,
            ProfilePicture = infouserCreated.ProfilePicture,
            Biography = infouserCreated.Biography,
            Country = infouserCreated.Country
        };
    }

    public async Task<UpdateResponse> Update(Guid Id, UpdateRequest infouserDto)
    {
        if (Id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty");
        }
        var existingInfo = await _InfoUser.GetById(Id);
        if (infouserDto.Biography != null && infouserDto.Country != null && infouserDto.ProfilePicture != null)
        {
            existingInfo.Biography = infouserDto.Biography;
            existingInfo.Country = infouserDto.Country;
            existingInfo.ProfilePicture = infouserDto.ProfilePicture;
        }
        await _InfoUser.Update(existingInfo);

        return new UpdateResponse
        {
            Biography = existingInfo.Biography,
            Country = existingInfo.Country,
            ProfilePicture = existingInfo.ProfilePicture
        };
    }

    public Task Delete(Guid Id)
    {
        if (Id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty");
        }
        return _InfoUser.Delete(Id);
    }

}