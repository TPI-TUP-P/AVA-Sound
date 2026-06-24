namespace Application.DTOs.ReproductionList.Response;

public class UpdateResponse
{
        public Guid Id { get; init; }
        public Guid IdUser {get; init;}
        public string? Name { get; set; }
        public string? Description {get; set;}
        public bool IsPublic {get; set;}

        public UpdateResponse(Guid id, Guid idUser, string? name, string? description, bool isPublic)
        {
            Id = id;
            IdUser = idUser;
            Name = name;
            Description = description;
            IsPublic = isPublic;
        }
}