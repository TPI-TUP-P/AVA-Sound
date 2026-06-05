using Domain.Objects.Statistics;

namespace Application.DTOs.Statistic.Response;



public class GetAllResponse
{
    public Guid Id { get;  init; }
    public Guid IdUser { get; init; }
  
    public List<SongReproduction> Reproductions { get; set; } = new();



    public GetAllResponse(Guid id, Guid idUser, List<SongReproduction> reproductions)
    {
        Id = id;
        IdUser = idUser;
        
        Reproductions = reproductions;
        
    }

    
}


