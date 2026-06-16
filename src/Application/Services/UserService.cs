
using Application.DTOs.User.request;
using Application.DTOs.User.Request;
using Application.DTOs.User.response;
using Application.DTOs.User.Response;
using Application.Interfaces;
using Domain.Entities;
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
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            IsArtist = user.IsArtist,
            DateRegister = user.DateRegister,
            Role = user.Role
        };
    }

    public async Task<GetByIdResponse> GetById(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            throw new FieldEmptyExcepction("Id");

        var user = await _user.GetById(id, cancellationToken);

        if (user == null)
            throw new NotFoundException("user");

        return new GetByIdResponse
        {
            Id = id,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            IsArtist = user.IsArtist,
            DateRegister = user.DateRegister,
            Role = user.Role

        };
    }


    public async Task<PagerResponse<GetByIdResponse>> GetAll(PagerRequest pagerRequest, CancellationToken cancellationToken)
    {
        var users = await _user.GetAll(pagerRequest.Page, pagerRequest.PageSize, cancellationToken);

        var userTotal = await _user.Count();

        var response = new PagerResponse<GetByIdResponse>
        {
            Users = users.Select(x => new GetByIdResponse
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                IsArtist = x.IsArtist,
                Role = x.Role
            }).ToList(),

            Page = pagerRequest.Page,

            PageSize = pagerRequest.PageSize,

            UserTotal = userTotal,

            PageTotal = (int)Math.Ceiling(
                userTotal / (double)pagerRequest.PageSize)
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
            userDto.IsArtist,
            userDto.Role!
        );

        await _user.Create(user, cancellationToken);

        return new CreateResponse
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            IsArtist = user.IsArtist,
            DateRegister = user.DateRegister,
            Role = user.Role
        };
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

        if(user.Id != userId)
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
        {
            Id = userId,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            IsArtist = user.IsArtist,
            DateRegister = user.DateRegister,
            Role = user.Role
        };
    }


    public async Task Delete(Guid Id, CancellationToken cancellationToken)
    {
        if (Id == Guid.Empty)
            throw new FieldEmptyExcepction("Id");

        var user = await _user.GetById(Id, cancellationToken);

        if (user == null)
            throw new NotFoundException("user");
        
        if(!user.IsActive)
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
        if (currentUser.Role != "superadmin")
        {
            throw new UnauthorizedAccessException("No autorizado");
        }

        var user = await _user.GetById(userId, cancellationToken);

        user.HandleAdmin();

        await _user.Update(user, cancellationToken);
    }

}