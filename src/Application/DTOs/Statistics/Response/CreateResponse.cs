using Domain.Objects.Statistics;

namespace Application.DTOs.Statistic.Response;


public class CreateResponse
{
    public Guid Id { get; init; }
    public Guid IdUser { get; init; }

    public List<SongReproduction> Reproductions { get; set; } = new();

    


    public CreateResponse(Guid id, Guid idUser, List<SongReproduction> reproductions)
    {
        Id = id;
        IdUser = idUser;
        Reproductions = reproductions.ToList();
        
        ;
    }

    
    
}