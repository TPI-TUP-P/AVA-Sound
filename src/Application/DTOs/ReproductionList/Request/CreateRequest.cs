namespace Application.DTOs.ReproductionList.Request;

public class CreateRequest
{
    public Guid IdUser {get; init;}
    public string Name {get; set;} =null!;
    public string Description {get; set;}= null!;
    public bool IsPublic {get; set;}
}