
using Application.DTOs.User.request;
using Application.DTOs.User.Request;
using Application.DTOs.User.response;
using Application.DTOs.User.Response;
using Application.Interfaces;
using Domain.Entities;
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
            throw new Exception("El email es obligatorio");

        var user = await _user.GetByEmail(email, cancellationToken);

        if (user is null)
            throw new Exception("El usuario no existe");

        return new GetByIdResponse
        {
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            IsArtist = user.IsArtist,
            DateRegister = user.DateRegister,
            Role = user.Role
        };
    }
    
    public async Task<GetByIdResponse> GetById(Guid Id, CancellationToken cancellationToken)
    {
        if (Id == Guid.Empty)
            throw new Exception("El id no existe");

        var user = await _user.GetById(Id, cancellationToken);

        if (user == null)
            throw new Exception("El usuario no existe");

        return new GetByIdResponse
        {
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            IsArtist = user.IsArtist,
            DateRegister = user.DateRegister,
            Role = user.Role

        };
    }


    public async Task<PagerResponse<GetByIdResponse>> GetAll(PagerRequest pagerRequest,CancellationToken cancellationToken)
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
            throw new Exception("Datos inválidos");
        }



        if (string.IsNullOrWhiteSpace(userDto.Name))
            throw new Exception("Name es obligatorio");

        if (string.IsNullOrWhiteSpace(userDto.Surname))
            throw new Exception("Surname es obligatorio");

        if (string.IsNullOrWhiteSpace(userDto.Email))
            throw new Exception("Email es obligatorio");

        if (string.IsNullOrWhiteSpace(userDto.Password))
            throw new Exception("Contraseña es obligatoria");

        if (string.IsNullOrWhiteSpace(userDto.Role))
            throw new Exception("Role es obligatorio");



        var user = new User(
            userDto.Name,
            userDto.Surname,
            userDto.Email,
            userDto.Password,
            userDto.IsArtist,
            userDto.Role
        );

        await _user.Create(user, cancellationToken);

        return new CreateResponse
        {
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            IsArtist = user.IsArtist,
            DateRegister = user.DateRegister,
            Role = user.Role
        };
    }

    public async Task<UpdateResponse> Update(Guid id, UpdateRequest userDto, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            throw new Exception("Id inválido");

        if (userDto == null)
            throw new Exception("Datos inválidos");

        var user = await _user.GetById(id, cancellationToken);

        if (user == null)
            throw new Exception("el usuario no existe");

        if (string.IsNullOrWhiteSpace(userDto.Name))
            throw new Exception("Name es obligatorio");

        if (string.IsNullOrWhiteSpace(userDto.Surname))
            throw new Exception("Surname es obligatorio");

        if (string.IsNullOrWhiteSpace(userDto.Email))
            throw new Exception("Email es obligatorio");

        if (string.IsNullOrWhiteSpace(userDto.Password))
            throw new Exception("Contraseña es obligatoria");

        if (string.IsNullOrWhiteSpace(userDto.Role))
            throw new Exception("Role es obligatorio");

        user.UpdateInfo(
            userDto.Name,
            userDto.Surname,
            userDto.Email,
            userDto.Password,
            userDto.IsArtist,
            userDto.Role
        );

        await _user.Update(user, cancellationToken);

        return new UpdateResponse
        {
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Password = user.Password,
            IsArtist = user.IsArtist,
            Role = user.Role
        };
    }


    public async Task Delete(Guid Id, CancellationToken cancellationToken)
    {
        if (Id == Guid.Empty)
            throw new Exception("el id no existe");

        var user = await _user.GetById(Id, cancellationToken);

        if (user == null)
            throw new Exception("el usuario no existe");

        await _user.Delete(Id, cancellationToken);
    }


}