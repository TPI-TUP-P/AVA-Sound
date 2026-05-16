
using Application.DTOs.User.request;
using Application.DTOs.User.response;
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

    public async Task<GetByIdResponse> GetById(Guid Id)
    {
        if (Id == Guid.Empty)
            throw new Exception("El id no existe");

        var user = await _user.GetById(Id);

        if (user == null)
            throw new Exception("El usuario no existe");

        return new GetByIdResponse
        {
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            IsArtista = user.IsArtista,
            DateRegister = user.DateRegister,
            Role = user.Role

        };
    }


    public async Task<List<GetAllResponse>> GetAll(CancellationToken cancellationToken)
    {
        var users = await _user.GetAll(cancellationToken);

        return users.Select(u => new GetAllResponse
        {
            Name = u.Name,
            Surname = u.Surname,
            Email = u.Email,
            IsArtista = u.IsArtista,
            DateRegister = u.DateRegister,
            Role = u.Role
        }).ToList();
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
            userDto.IsArtista,
            userDto.Role
        );

        await _user.Create(user, cancellationToken);

        return new CreateResponse
        {
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            IsArtista = user.IsArtista,
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

        var user = await _user.GetById(id);

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
            userDto.IsArtista,
            userDto.Role
        );

        await _user.Update(user, cancellationToken);

        return new UpdateResponse
        {
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Password = user.Password,
            IsArtista = user.IsArtista,
            Role = user.Role
        };
    }


    public async Task Delete(Guid Id)
    {
        if (Id == Guid.Empty)
            throw new Exception("el id no existe");

        var user = await _user.GetById(Id);

        if (user == null)
            throw new Exception("el usuario no existe");

        await _user.Delete(Id);
    }


}