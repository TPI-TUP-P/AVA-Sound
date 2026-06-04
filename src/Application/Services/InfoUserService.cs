using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Application.DTOs.InfoUser.Request;
using Application.DTOs.InfoUser.Response;
using Domain.Exceptions;
namespace Application.Services;

public class InfoUserService : IInfoUserService
{
    private readonly IInfoUserRepository _InfoUser;
    private readonly IUserRepository _user;
    public InfoUserService(IInfoUserRepository infouser, IUserRepository user)
    {
        _InfoUser = infouser;
        _user = user;
    }

    public async Task<GetByIdResponse> GetById(Guid Id, CancellationToken cancellationToken)
    {
        ValidateId(Id);

        var infouser = await _InfoUser.GetById(Id, cancellationToken);
        if (infouser is null)
        {
            throw new NotFoundException("InfoUser");
        }
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
        if (infouserDto is null)
        {
            throw new FieldEmptyExcepction(nameof(infouserDto));
        }
        if (infouserDto.ProfilePicture is null)
        {
            throw new FieldEmptyExcepction("Profile Picture");
        }
        if (string.IsNullOrWhiteSpace(infouserDto.Biography))
        {
            throw new FieldEmptyExcepction(nameof(infouserDto.Biography));
        }

        if (string.IsNullOrWhiteSpace(infouserDto.Country))
        {
            throw new FieldEmptyExcepction(nameof(infouserDto.Country));
        }
        var userExist = await _user.GetById(infouserDto.IdUser, cancellationToken);
        if (userExist is null)
        {
            throw new NotFoundException("User");
        }
        var existingInfoUser = await _InfoUser.GetById(infouserDto.IdUser, cancellationToken);
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

    public async Task<UpdateResponse> Update(Guid Id, Guid IdUser, UpdateRequest infouserDto, CancellationToken cancellationToken)
    {
        var userExist = await _user.GetById(infouserDto.IdUser, cancellationToken);
        if (userExist is null)
        {
            throw new NotFoundException("User");
        }
        ValidateId(Id);
        var existingInfo = await _InfoUser.GetById(Id, cancellationToken);
        if (existingInfo is null)
        {
            throw new NotFoundException("InfoUser");
        }
        if (existingInfo.IdUser != IdUser)
        {
            throw new IdNotMatchException();
        }
        if (infouserDto.Biography != null)
        {
            existingInfo.Biography = infouserDto.Biography;
        }
        if (infouserDto.Country != null)
        {
            existingInfo.Country = infouserDto.Country;
        }

        if (infouserDto.ProfilePicture != null)
        {
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

    public async Task Delete(Guid Id, Guid IdUser, CancellationToken cancellationToken)
    {
        ValidateId(Id);
        var existingInfo = await _InfoUser.GetById(Id, cancellationToken);
        if (existingInfo is null)
        {
            throw new NotFoundException("InfoUser");
        }
        if (existingInfo.IdUser != IdUser)
        {
            throw new IdNotMatchException();
        }

        await _InfoUser.Delete(Id, cancellationToken);
    }

    private static void ValidateId(Guid id)
    {
        if (id == Guid.Empty)
            throw new FieldEmptyExcepction("Id");
    }

}