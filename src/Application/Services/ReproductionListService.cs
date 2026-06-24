using System.ComponentModel;
using Application.DTOs.ReproductionList.Request;
using Application.DTOs.ReproductionList.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;


namespace Application.Services;

public class ReproductionListService : IReproductionListService
{
    private IReproductionsListRepository _reproductionList;
    private ISongRepository _song;
    private IUserRepository _user;
    public ReproductionListService(IReproductionsListRepository reproductionList, ISongRepository song, IUserRepository user)
    {
        _reproductionList = reproductionList;
        _song = song;
        _user = user;
    }

    public async Task<GetByIdResponse> GetById(Guid Id, Guid idUser, CancellationToken cancellationToken)
    {
        if (Id == Guid.Empty)
        {
            throw new FieldEmptyExcepction("Id");
        }

        var reproductionsList = await _reproductionList.GetById(Id, cancellationToken);
        var user = await _user.GetById(idUser, cancellationToken);

        if (!reproductionsList.IsPublic && user.Role == UserRole.User && reproductionsList.IdUser != idUser){  
            throw new  NotFoundException("reproduction list");
        }
    

        if (reproductionsList == null)
        {
            throw new NotFoundException("reproduction list");
        }

        return new GetByIdResponse
        (
            reproductionsList.Id,
            reproductionsList.IdUser,
            reproductionsList.Name!,
            reproductionsList.Description!,
            reproductionsList.IsPublic,
            reproductionsList.Creation,
            reproductionsList.Songs.Select(s => new SongResponse
            (
                s.Id,
                s.Title,
                s.Artist.Id
            )).ToList()
        );
    }

    // public async Task<List<GetAllResponse>> GetAll(CancellationToken cancellationToken)
    // {
    //     var reproductionsLists = await _reproductionList.GetAll(cancellationToken);

    //     return reproductionsLists.Select(list => new GetAllResponse(
    //         list.Id,
    //         list.IdUser,
    //         list.Name,
    //         list.Description,
    //         list.IsPublic,
    //         list.Creation,
    //         list.Songs.Count
    //     )).ToList();
    // }


    public async Task<UpdateResponse> Update(Guid id, UpdateRequest updateRequest, Guid idUser, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            throw new FieldEmptyExcepction("Id");
        }
        if (updateRequest is null)
        {
            throw new FieldEmptyExcepction("updateRequest");
        }
        if (updateRequest.Name!.Length < 2 )
        {
            throw new FieldIsNotLongException("Name", 2);
        }
        if(updateRequest.Name.Length > 50)
        {
            throw new FieldTooLongException("Name", 50);
        }
        if (updateRequest.Description!.Length < 5)
        {
            throw new FieldIsNotLongException("Description", 5);
        }
        if(updateRequest.Description.Length > 200)
        {
            throw new FieldTooLongException("Description", 200);
        }



         var reproductionsList= await _reproductionList.GetById(id, cancellationToken);

         

        //  if (
        // reproductionsList.Name == updateRequest.Name &&
        // reproductionsList.Description == updateRequest.Description &&
        // reproductionsList.IsPublic == updateRequest.IsPublic
        // )
        // {
        // throw new Exception("No hay cambios para actualizar");
        // }

         

        if (reproductionsList == null)
        {
            throw new NotFoundException("reproduction list");
        }

        var user = await _user.GetById(idUser, cancellationToken);
        
        if(idUser!=reproductionsList.IdUser && user.Role == UserRole.User)
            throw new UnauthorizedAccessException("No authorized");


        reproductionsList.UpdateInfo(
            updateRequest.Name,
            updateRequest.Description,
            updateRequest.IsPublic
        );


         var result= new UpdateResponse
         (
             id,
             reproductionsList.IdUser,
             reproductionsList.Name,
             reproductionsList.Description,
             reproductionsList.IsPublic
         );

        await _reproductionList.Update(reproductionsList, cancellationToken);

         return result;
        
    }





    public async Task<CreateResponse> Create(Guid idUser, CreateRequest reproductionListDto, CancellationToken cancellationToken)
    {

        if (reproductionListDto == null)
            throw new FieldEmptyExcepction(nameof(reproductionListDto));

        // if (reproductionListDto.IdUser == Guid.Empty)
        //     throw new FieldEmptyExcepction("IdUser");
            

        if (string.IsNullOrWhiteSpace(reproductionListDto.Name))
            throw new FieldEmptyExcepction("Name");


        if (string.IsNullOrWhiteSpace(reproductionListDto.Description))
            throw new FieldEmptyExcepction("Description");

            if (reproductionListDto.Name.Length < 2 )
        {
            throw new FieldIsNotLongException("Name", 2);
        }
        if(reproductionListDto.Name.Length > 50)
        {
            throw new FieldTooLongException("Name", 50);
        }
        if (reproductionListDto.Description.Length < 5)
        {
            throw new FieldIsNotLongException("Description", 5);
        }
        if(reproductionListDto.Description.Length > 200)
        {
            throw new FieldTooLongException("Description", 200);
        }



        var reproductionListData = new ReproductionsList(
            idUser,
            reproductionListDto.Name,
            reproductionListDto.Description,
            reproductionListDto.IsPublic
        );

        var reproductionListCreated = await _reproductionList.Create(reproductionListData, cancellationToken);

        return new CreateResponse
        (
            reproductionListCreated.Id,
            reproductionListCreated.IdUser,
            reproductionListCreated.Name,
            reproductionListCreated.Description,
            reproductionListCreated.IsPublic
        );
    }

    // public async Task AddSong(Guid listId, Guid songId, CancellationToken cancellationToken)
    // {
    //     if (listId == Guid.Empty)
    //         throw new ArgumentException("listId inválido");

    //     if (songId == Guid.Empty)
    //         throw new FieldEmptyExcepction("songId");


    //     var list = await _reproductionList.GetById(listId, cancellationToken);

    //     if (list == null)
    //         throw new NotFoundException("Reproduction List");

    //     var song = await _song.GetById(songId, cancellationToken);

    //     if (song == null)
    //         throw new NotFoundException("Song");


    //     list.AddSong(song);

    //     await _reproductionList.Update(list, cancellationToken);
    // }


    public async Task AddSong(Guid listId, Guid songId, Guid idUser, CancellationToken cancellationToken)
    {
        if (listId == Guid.Empty)
            throw new ArgumentException("listId inválido");

        if (songId == Guid.Empty)
            throw new FieldEmptyExcepction("songId");

        var list = await _reproductionList.GetById(listId, cancellationToken);

        

        if (list == null)
            throw new NotFoundException("Reproduction List");
            
        if(idUser!=list.IdUser)
            throw new IdNotMatchException();

        var song = await _song.GetById(songId, cancellationToken);

        if (song == null)
            throw new NotFoundException("Song");

        if(list.Songs.Any(s=>s.Id==songId))
            throw new AlreadyExistExcepction("Song in list", song.Title);

        list.AddSong(song);

        await _reproductionList.Update(list, cancellationToken);
    }
    




    // public async Task RemoveSong(Guid listId, Guid songId, CancellationToken cancellationToken)
    // {
    //     if (listId == Guid.Empty)
    //         throw new ArgumentException("listId inválido");

    //     if (songId == Guid.Empty)
    //         throw new ArgumentException("songId inválido");

    //     var list = await _reproductionList.GetById(listId, cancellationToken);

    //     if (list == null)
    //         throw new KeyNotFoundException("La lista no existe");

    //     var song = list.Songs.FirstOrDefault(s => s.Id == songId);

    //     if (song == null)
    //         throw new KeyNotFoundException("La canción no está en la lista");

    //     list.RemoveSong(song);

    //     await _reproductionList.Update(list, cancellationToken);
    // }

    public async Task RemoveSong(Guid listId, Guid songId, Guid idUser, CancellationToken cancellationToken)
    {
        if (listId == Guid.Empty)
            throw new FieldEmptyExcepction("list Id");

        if (songId == Guid.Empty)
            throw new FieldEmptyExcepction("song Id");;

        var list = await _reproductionList.GetById(listId, cancellationToken);

        if (list == null)
            throw new NotFoundException("Reproduction List");

        if(idUser!=list.IdUser)
            throw new IdNotMatchException();
        

        var song = list.Songs.FirstOrDefault(s => s.Id == songId);

        if (song == null)
            throw new NotFoundException("Song in list");

        if(!list.Songs.Any(s=>s.Id==songId))
            throw new NotFoundException("Song in list");

        list.RemoveSong(song);

        await _reproductionList.Update(list, cancellationToken);
    }
    

    public async Task<List<GetAllResponse>> GetByIdUser(Guid idUser, CancellationToken cancellationToken)
    {
        if (idUser == Guid.Empty)
            throw new FieldEmptyExcepction("Id");

        var lists = await _reproductionList.GetByIdUser(idUser, cancellationToken);

        return lists.Select(list => new GetAllResponse(
            list.Id,
            list.IdUser,
            list.Name!,
            list.Description!,
            list.IsPublic,
            list.Creation,
            list.Songs.Count
        )).ToList();
        
        
    }

    public async Task Delete(Guid id, Guid idUser, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            throw new FieldEmptyExcepction("Id");

        var list = await _reproductionList.GetById(id, cancellationToken);

        if (list == null)
            throw new NotFoundException("Reproduction List");

        var user = await _user.GetById(idUser, cancellationToken);

        if(idUser!=list.IdUser && user.Role == UserRole.User)
            throw new UnauthorizedAccessException("No authorized");

        await _reproductionList.Delete(id, cancellationToken);
    }

}