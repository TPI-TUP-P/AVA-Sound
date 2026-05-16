using System.ComponentModel;
using Application.DTOs.ReproductionList.Request;
using Application.DTOs.ReproductionList.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;


namespace Application.Services;

public class ReproductionListService : IReproductionListService
{
    private IReproductionsListRepository _reproductionList;
    private ISongRepository _song;
    public ReproductionListService(IReproductionsListRepository reproductionList, ISongRepository song)
    {
        _reproductionList = reproductionList;
        _song = song;
    }

    public async Task<GetByIdResponse> GetById(Guid Id)
    {
        if (Id == Guid.Empty)
        {
            throw new Exception("Id es vacio");
        }

        var reproductionsList = await _reproductionList.GetById(Id);

        if (reproductionsList == null)
        {
            throw new Exception("La lista no existe");
        }

        return new GetByIdResponse
        {
            Id = reproductionsList.Id,
            IdUser = reproductionsList.IdUser,
            Name = reproductionsList.Name,
            Description = reproductionsList.Description,
            IsPublic = reproductionsList.IsPublic,
            Creation = reproductionsList.Creation,
            Songs = reproductionsList.Songs.Select(s => new SongResponse
            {
                Id = s.Id,
                Title = s.Title
            }).ToList()
        };
    }





    public async Task<CreateResponse> Create(CreateRequest reproductionListDto, CancellationToken cancellationToken)
    {

        if (reproductionListDto == null)
            throw new Exception("debe existir la lista");

        if (reproductionListDto.IdUser == Guid.Empty)
            throw new Exception("IdUser es obligatorio");

        if (string.IsNullOrWhiteSpace(reproductionListDto.Name))
            throw new Exception("Name es obligatorio");

        if (string.IsNullOrWhiteSpace(reproductionListDto.Description))
            throw new Exception("Description es obligatoria");

        var reproductionListData = new ReproductionsList(
            reproductionListDto.IdUser,
            reproductionListDto.Name,
            reproductionListDto.Description,
            reproductionListDto.IsPublic
        );

        var reproductionListCreated = await _reproductionList.Create(reproductionListData, cancellationToken);

        return new CreateResponse
        {
            Id = reproductionListCreated.Id,
            IdUser = reproductionListCreated.IdUser,
            Name = reproductionListCreated.Name,
            Description = reproductionListCreated.Description,
            IsPublic = reproductionListCreated.IsPublic
        };
    }

    public async Task AddSong(Guid listId, Guid songId, CancellationToken cancellationToken)
    {
        if (listId == Guid.Empty)
            throw new ArgumentException("listId inválido");

        if (songId == Guid.Empty)
            throw new ArgumentException("songId inválido");

        var list = await _reproductionList.GetById(listId);

        if (list == null)
            throw new KeyNotFoundException("La lista no existe");

        var song = await _song.GetById(songId);

        if (song == null)
            throw new KeyNotFoundException("La canción no existe");

        list.AddSong(song);

        await _reproductionList.Update(list, cancellationToken);
    }


    public async Task RemoveSong(Guid listId, Guid songId, CancellationToken cancellationToken)
    {
        if (listId == Guid.Empty)
            throw new ArgumentException("listId inválido");

        if (songId == Guid.Empty)
            throw new ArgumentException("songId inválido");

        var list = await _reproductionList.GetById(listId);

        if (list == null)
            throw new KeyNotFoundException("La lista no existe");

        var song = list.Songs.FirstOrDefault(s => s.Id == songId);

        if (song == null)
            throw new KeyNotFoundException("La canción no está en la lista");

        list.RemoveSong(song);

        await _reproductionList.Update(list, cancellationToken);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido");

        var list = await _reproductionList.GetById(id);

        if (list == null)
            throw new KeyNotFoundException("La lista no existe");

        await _reproductionList.Delete(id, cancellationToken);
    }

}