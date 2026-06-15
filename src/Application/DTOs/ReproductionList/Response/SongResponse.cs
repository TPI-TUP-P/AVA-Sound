namespace Application.DTOs.ReproductionList.Response;


public class SongResponse
{
    public Guid Id { get; init; }
        public string? Title { get; set; }
        public Guid Artist { get; set;}
}