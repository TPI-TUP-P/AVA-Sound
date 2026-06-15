namespace Application.DTOs.ReproductionList.Response;

public class CreateResponse
{
        public Guid Id { get; init; }
        public Guid IdUser {get; init;}
        public string Name { get; set; }=null!;
        public string Description {get; set;}=null!;
        public bool IsPublic {get; set;}
}