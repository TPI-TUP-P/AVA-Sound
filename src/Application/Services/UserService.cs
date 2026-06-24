
using Application.DTOs.User.request;
using Application.DTOs.User.Request;
using Application.DTOs.User.response;
using Application.DTOs.User.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;


namespace Application.Services;

public class UserService : IUserService
{
    private IUserRepository _user;
    public UserService(IUserRepository user)
    {
        _user = user;
    }



    public async Task<GetByIdResponse> GetByEmail(string email, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new FieldEmptyExcepction("Email");

        var user = await _user.GetByEmail(email, cancellationToken);

        if (user is null)
            throw new NotFoundException("user");

        return new GetByIdResponse
        (
            user.Id,
            user.Name,
            user.Surname,
            user.Email,
            user.IsArtist,
            user.DateRegister,
            user.Role
        );
    }

    public async Task<GetByIdResponse> GetById(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            throw new FieldEmptyExcepction("Id");

        var user = await _user.GetById(id, cancellationToken);

        if (user == null)
            throw new NotFoundException("user");

        return new GetByIdResponse
        (
            user.Id,
            user.Name,
            user.Surname,
            user.Email,
            user.IsArtist,
            user.DateRegister,
            user.Role
        );
    }


    public async Task<PagerResponse<GetAllResponse>> GetAll( CancellationToken cancellationToken)
    {
        var users = await _user.GetAll(1, 10, cancellationToken);

        var userTotal = await _user.Count();

        var response = new PagerResponse<GetAllResponse>
        {
            Users = users.Select(x => new GetAllResponse
            (
                x.Id,
                x.Name,
                x.Surname,
                x.Email,
                x.IsArtist,
                x.DateRegister,
                x.Role
            )).ToList(),

            Page = 1,

            PageSize = 10,

            UserTotal = userTotal,

            PageTotal = (int)Math.Ceiling(
                userTotal / (double)10)
        };

        return response;
    }


    public async Task<CreateResponse> Create(CreateRequest userDto, CancellationToken cancellationToken)
    {
        if (userDto == null)
        {
            throw new FieldEmptyExcepction("User data");
        }



        if (string.IsNullOrWhiteSpace(userDto.Name))
            throw new FieldEmptyExcepction("Name");

        if (string.IsNullOrWhiteSpace(userDto.Surname))
            throw new FieldEmptyExcepction("Surname");

        if (string.IsNullOrWhiteSpace(userDto.Email))
            throw new FieldEmptyExcepction("Email");

        if (string.IsNullOrWhiteSpace(userDto.Password))
            throw new FieldEmptyExcepction("Password");




        var user = new User(
            userDto.Name,
            userDto.Surname,
            userDto.Email,
            userDto.Password,
            userDto.IsArtist
        );

        await _user.Create(user, cancellationToken);

        return new CreateResponse
        (
            user.Id,
            user.Name,
            user.Surname,
            user.Email,
            user.IsArtist,
            user.DateRegister,
            user.Role
        );
    }

    public async Task<UpdateResponse> Update(Guid id, UpdateRequest userDto, Guid userId, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            throw new FieldEmptyExcepction("Id");

        if (userDto == null)
            throw new FieldEmptyExcepction("userDto");

        var user = await _user.GetById(id, cancellationToken);

        if (user == null)
            throw new NotFoundException("user");

        if (user.Id != userId)
            throw new IdNotMatchException();

        if (string.IsNullOrWhiteSpace(userDto.Name))
            throw new FieldEmptyExcepction("Name");

        if (string.IsNullOrWhiteSpace(userDto.Surname))
            throw new FieldEmptyExcepction("Surname");

        if (string.IsNullOrWhiteSpace(userDto.Email))
            throw new FieldEmptyExcepction("Email");

        if (string.IsNullOrWhiteSpace(userDto.Password))
            throw new FieldEmptyExcepction("Password");


        user.UpdateInfo(
            userDto.Name,
            userDto.Surname,
            userDto.Email,
            userDto.Password,
            userDto.IsArtist
        );

        await _user.Update(user, cancellationToken);

        return new UpdateResponse
        (
            userId,
            user.Name,
            user.Surname,
            user.Email,
            user.IsArtist,
            user.DateRegister,
            user.Role
        );
    }


    public async Task Delete(Guid Id, Guid userId, CancellationToken cancellationToken)
    {
        if (Id == Guid.Empty)
            throw new FieldEmptyExcepction("Id");

        var user = await _user.GetById(Id, cancellationToken);

        if (user == null)
            throw new NotFoundException("user");

        var currentUser = await _user.GetById(userId, cancellationToken);

        if (user.Id != userId && currentUser.Role == UserRole.User)
            throw new UnauthorizedAccessException("No authorized");

        if (!user.IsActive)
        {
            throw new UserNotActivateException();
        }

        user.HandleActivate();
        await _user.Update(user, cancellationToken);
    }

    public async Task HandleAdmin(Guid userId, Guid currentUserId, CancellationToken cancellationToken)
    {
        var currentUser = await _user.GetById(currentUserId, cancellationToken);

        if (currentUser == null)
        {
            throw new NotFoundException("current user");
        }
        if (currentUser.Role != UserRole.SuperAdmin)
        {
            throw new UnauthorizedAccessException("No authorized");
        }

        var user = await _user.GetById(userId, cancellationToken);

        user.HandleAdmin();

        await _user.Update(user, cancellationToken);
    }

}