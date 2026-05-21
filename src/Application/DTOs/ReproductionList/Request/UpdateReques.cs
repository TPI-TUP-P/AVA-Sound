namespace Application.DTOs.ReproductionList.Request;

public class UpdateRequest
{
    public string Name {get; set;} =null!;
    public string Description {get; set;}= null!;
    public bool IsPublic {get; set;}
}