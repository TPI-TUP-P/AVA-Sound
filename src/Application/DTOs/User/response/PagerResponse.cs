namespace Application.DTOs.User.response;

public class PagerResponse<T>
{
    public List<T> Users { get; set; } = new();

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalRecords { get; set; }

    public int TotalPages { get; set; }
}