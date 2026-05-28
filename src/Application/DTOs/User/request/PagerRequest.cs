namespace Application.DTOs.User.request;

public class PagerRequest
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}