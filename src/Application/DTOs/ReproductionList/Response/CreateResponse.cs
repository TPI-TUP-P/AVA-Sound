namespace Application.DTOs.ReproductionList.Response;

public class CreateResponse
{
        public Guid Id { get; init; }
        public Guid IdUser {get; init;}
        public string? Name { get; set; }
        public string? Description {get; set;}
        public bool IsPublic {get; set;}

        public CreateResponse(Guid id, Guid idUser, string? name, string? description, bool isPublic)
        {
            Id = id;
            IdUser = idUser;
            Name = name;
            Description = description;
            IsPublic = isPublic;
        }
}