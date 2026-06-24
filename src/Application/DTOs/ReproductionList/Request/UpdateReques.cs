namespace Application.DTOs.ReproductionList.Request;

public class UpdateRequest
{
    // public int Id {get; init;}
    public string? Name {get; set;}
    public string? Description {get; set;}
    public bool IsPublic {get; set;}
}