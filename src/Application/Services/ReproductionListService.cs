using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class ReproductionListService : IReproductionListService
{
    private static List<ReproductionsList> _lists = new();

    public Task<ReproductionsList?> GetById(Guid id)
    {
        var list = _lists.FirstOrDefault(l => l.Id == id);
        return Task.FromResult(list);
    }

    public Task<ReproductionsList> Create(ReproductionsList list)
    {
        _lists.Add(list);
        return Task.FromResult(list);
    }

    public Task<ReproductionsList> AddSong(Guid id, Song song)
    {
        var list = _lists.FirstOrDefault(l => l.Id == id);
        if (list == null)
            throw new Exception("Reproduction list not found");
        list.AddSong(song);
        return Task.FromResult(list);
    }

    public Task<ReproductionsList> DeleteSong(Guid id, Song song)
    {
        var list = _lists.FirstOrDefault(l => l.Id == id);
        if (list == null)
            throw new Exception("Reproduction list not found");
        list.RemoveSong(song);
        return Task.FromResult(list);
    }

    public Task Delete(Guid id)
    {
        var list = _lists.FirstOrDefault(l => l.Id == id);
        if (list == null)
            throw new Exception("Reproduction list not found");
        _lists.Remove(list);
        return Task.CompletedTask;
    }
}