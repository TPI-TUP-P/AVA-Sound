using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ReproductionsListRepository : IReproductionsListRepository
{
    private readonly ApplicationContext _context;

    public ReproductionsListRepository(ApplicationContext context)
    {
        _context = context;
    }


    public async Task<ReproductionsList> GetById(Guid id, CancellationToken cancellationToken)
    {
        var lista = await _context.ReproductionsLists.Include(r => r.Songs)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
            
        return lista!;
    }


   public async Task<List<ReproductionsList>> GetByIdUser(Guid idUser, CancellationToken cancellationToken)
{
    return await _context.ReproductionsLists
            .Where(r => r.IdUser == idUser)

        .Include(r => r.Songs) 
        .ToListAsync(cancellationToken);
}
    public async Task<ReproductionsList> Create(ReproductionsList list, CancellationToken cancellationToken)
    {
        _context.ReproductionsLists.Add(list);
        await _context.SaveChangesAsync();
        return list;
    }

    public async Task<ReproductionsList> Update(ReproductionsList list, CancellationToken cancellationToken)
    {
        _context.ReproductionsLists.Update(list);
        await _context.SaveChangesAsync(cancellationToken);
        return list;
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var list = await _context.ReproductionsLists.FindAsync(id);

        if (list != null)
        {
            _context.ReproductionsLists.Remove(list);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }


    public async Task<ReproductionsList> AddSong(Guid id, Song song)
    {
        var list = await _context.ReproductionsLists.FindAsync(id);
        list!.AddSong(song);

        await _context.SaveChangesAsync();

        return list;
    }


    public async Task<ReproductionsList> RemoveSong(Guid id, Song song)
    {
        var list = await _context.ReproductionsLists.Include(r => r.Songs)
            .FirstOrDefaultAsync(r => r.Id == id);

        list!.RemoveSong(song);

        await _context.SaveChangesAsync();

        return list;
        

    }

}