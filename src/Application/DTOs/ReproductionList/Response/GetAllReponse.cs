namespace Application.DTOs.ReproductionList.Response;

public class GetAllResponse
{
    public Guid Id { get; set; }
    public Guid IdUser { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsPublic { get; set; }
    public DateTime Creation { get; set; }
    public int SongCount { get; set; }


    public GetAllResponse(Guid id, Guid idUser, string name, string description, bool isPublic, DateTime creation, int songCount)
    {
        Id = id;
        IdUser = idUser;
        Name = name;
        Description = description;
        IsPublic = isPublic;
        Creation = creation;
        SongCount = songCount;
    }
}
