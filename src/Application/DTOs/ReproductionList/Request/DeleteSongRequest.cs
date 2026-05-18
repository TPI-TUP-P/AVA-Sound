namespace Application.DTOs.ReproductionList.Request;

public class DeleteSongRequest
{
    public Guid ReproductionListId { get; set; }
        public Guid SongId { get; set; }
}