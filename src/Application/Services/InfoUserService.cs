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
    private readonly IUserService _user;
    public InfoUserService(IInfoUserRepository infouser, IUserService user)
    {
        _InfoUser = infouser;
        _user = user;
    }

    public async Task<GetByIdResponse> GetById(Guid Id, CancellationToken cancellationToken)
    {
        ValidateId(Id);

        var infourser = await _InfoUser.GetById(Id, cancellationToken);
        if (infourser is null)
        {
            throw new NotFoundException("InfoUser");
        }
        Console.WriteLine(infourser);
        return new GetByIdResponse
        (
            infourser.Id,
           infourser.IdUser,
            infourser.ProfilePicture!,
         infourser.Biography!,
           infourser.Country!
        )
        ;
    }

    public async Task<CreateResponse> Create(CreateRequest infouserDto, Guid idUser, CancellationToken cancellationToken)
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
        var userExist = await _user.GetById(idUser, cancellationToken);
        if (userExist is null)
        {
            throw new NotFoundException("User");
        }
        ValidateId(idUser);
        var existingInfoUser = await _InfoUser.GetById(idUser, cancellationToken);
        if (existingInfoUser is not null)
        {
            throw new AlreadyExistExcepction("infouser", "user");
        }
        var newInfoUser = new InfoUser(
            idUser,
            infouserDto.ProfilePicture,
            infouserDto.Biography,
            infouserDto.Country
        );

        var infouserCreated = await _InfoUser.Create(newInfoUser, cancellationToken);
        return new CreateResponse
        (
            infouserCreated.Id,
           infouserCreated.IdUser,
            infouserCreated.ProfilePicture!,
           infouserCreated.Biography!,
           infouserCreated.Country!
        );
    }

    public async Task<UpdateResponse> Update(Guid Id, Guid IdUser, UpdateRequest infouserDto, CancellationToken cancellationToken)
    {
        var userExist = await _user.GetById(IdUser, cancellationToken);
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
            throw new ForbiddenException("InfoUser");
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
        existingInfo.UpdateInfoUser(infouserDto.ProfilePicture!, infouserDto.Biography!, infouserDto.Country!);
        await _InfoUser.Update(existingInfo, cancellationToken);

        return new UpdateResponse
        (
            IdUser,
            existingInfo.Biography!,
            existingInfo.Country!,
            existingInfo.ProfilePicture!
       );
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
    // validate id its more simple! maldito teni.
    private static void ValidateId(Guid id)
    {
        if (id == Guid.Empty)
            throw new FieldEmptyExcepction("Id");
    }

}