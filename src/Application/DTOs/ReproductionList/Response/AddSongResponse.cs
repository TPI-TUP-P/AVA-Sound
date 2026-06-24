namespace Application.DTOs.ReproductionList.Response;

public class AddSongResponse
{
    public Guid ReproductionListId { get; set; }
    public Guid SongId { get; set; }

    public AddSongResponse(Guid reproductionListId, Guid songId)
    {
        ReproductionListId = reproductionListId;
        SongId = songId;
    }
    
}
