namespace Application.DTOs.ReproductionList.Request;

public class CreateRequest
{
    // public Guid IdUser {get; init;}
    public string? Name {get; set;}
    public string? Description {get; set;}
    public bool IsPublic {get; set;}
}