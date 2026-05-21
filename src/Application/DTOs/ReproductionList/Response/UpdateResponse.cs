namespace Application.DTOs.ReproductionList.Response;

public class UpdateResponse
{
        public Guid Id { get; set; }
        public Guid IdUser {get; set;}
        public string Name { get; set; }=null!;
        public string Description {get; set;}=null!;
        public bool IsPublic {get; set;}
}