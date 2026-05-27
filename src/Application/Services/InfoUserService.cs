using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Application.DTOs.InfoUser.Request;
using Application.DTOs.InfoUser.Response;
using Domain.Exceptions;
namespace Application.Services;

public class InfoUserService : IInfoUserService
{
    private IInfoUserRepository _InfoUser;
    private IUserRepository _user;
    public InfoUserService(IInfoUserRepository infouser, IUserRepository user)
    {
        _InfoUser = infouser;
        _user = user;
    }

    public async Task<GetByIdResponse> GetById(Guid Id, CancellationToken cancellationToken)
    {
        if (Id == Guid.Empty)
        {
            throw new FieldEmptyExcepction("Id");
        }

        var infouser = await _InfoUser.GetById(Id, cancellationToken);

        return new GetByIdResponse
        {
            IdUser = infouser.IdUser,
            ProfilePicture = infouser.ProfilePicture,
            Biography = infouser.Biography,
            Country = infouser.Country
        }
        ;
    }

    public async Task<CreateResponse> Create(CreateRequest infouserDto, CancellationToken cancellationToken)
    {
        var userExist = _user.GetById(infouserDto.IdUser, cancellationToken);
        if (userExist is null)
        {
            throw new NotFoundException("User");
        }
        if (infouserDto == null)
        {
            throw new FieldEmptyExcepction("Fields");
        }
        if (infouserDto.ProfilePicture is null)
        {
            throw new FieldEmptyExcepction("Profile Picture");
        }
        if (infouserDto.Biography is null || infouserDto.Country is null)
        {
            throw new FieldEmptyExcepction("Biography");
        }
        var existingInfoUser = _InfoUser.GetById(infouserDto.IdUser, cancellationToken);
        if (existingInfoUser is not null)
        {
            throw new AlreadyExistExcepction("infouser", "user");
        }
        var newInfoUser = new InfoUser(
            infouserDto.IdUser,
            infouserDto.ProfilePicture,
            infouserDto.Biography,
            infouserDto.Country
        );

        var infouserCreated = await _InfoUser.Create(newInfoUser, cancellationToken);
        return new CreateResponse
        {
            IdUser = infouserCreated.IdUser,
            ProfilePicture = infouserCreated.ProfilePicture,
            Biography = infouserCreated.Biography,
            Country = infouserCreated.Country
        };
    }

    public async Task<UpdateResponse> Update(Guid Id, UpdateRequest infouserDto, CancellationToken cancellationToken)
    {
        var userExist = _user.GetById(infouserDto.IdUser, cancellationToken);
        if (userExist is null)
        {
            throw new NotFoundException("User");
        }
        if (Id == Guid.Empty)
        {
            throw new FieldEmptyExcepction("Id");
        }
        var existingInfo = await _InfoUser.GetById(Id, cancellationToken);
        if (infouserDto.Biography != null && infouserDto.Country != null && infouserDto.ProfilePicture != null)
        {
            existingInfo.Biography = infouserDto.Biography;
            existingInfo.Country = infouserDto.Country;
            existingInfo.ProfilePicture = infouserDto.ProfilePicture;
        }
        await _InfoUser.Update(existingInfo, cancellationToken);

        return new UpdateResponse
        {
            Biography = existingInfo.Biography,
            Country = existingInfo.Country,
            ProfilePicture = existingInfo.ProfilePicture
        };
    }

    public Task Delete(Guid Id, CancellationToken cancellationToken)
    {
        if (Id == Guid.Empty)
        {
            throw new FieldEmptyExcepction("Id");
        }
        return _InfoUser.Delete(Id, cancellationToken);
    }

}