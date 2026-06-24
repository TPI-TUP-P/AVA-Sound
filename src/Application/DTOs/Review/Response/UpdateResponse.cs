namespace Application.DTOs.Review.Response;

public class UpdateResponse
{
    public string? Comment { get; set; }

    public UpdateResponse(string comment)
    {
        Comment = comment;
    }
}