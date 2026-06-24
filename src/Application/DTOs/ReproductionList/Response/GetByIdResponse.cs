namespace Application.DTOs.ReproductionList.Response;
using Application.DTOs.ReproductionList.Response;

public class GetByIdResponse
{
    public Guid Id { get; init; }
        public Guid IdUser { get; init; }
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public bool IsPublic { get; set; }
        public DateTime Creation { get; set; }
        public List<SongResponse> Songs { get; set; } = new();


    public GetByIdResponse(Guid id, Guid idUser, string name, string description, bool isPublic, DateTime creation, List<SongResponse> songs)
    {
        Id = id;
        IdUser = idUser;
        Name = name;
        Description = description;
        IsPublic = isPublic;
        Creation = creation;
        Songs = songs;
    }
}