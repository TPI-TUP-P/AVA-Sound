namespace Application.DTOs.ReproductionList.Response;


public class SongResponse
{
    public Guid Id { get; init; }
        public string? Title { get; set; }
        public Guid Artist { get; set;}

        public SongResponse(Guid id, string? title, Guid artist)
        {
            Id = id;
            Title = title;
            Artist = artist;
        }
}