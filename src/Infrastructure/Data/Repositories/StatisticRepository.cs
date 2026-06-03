namespace Infrastructure.Data.Repositories;

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

public class StatisticRepository : IStatisticRepository
{
    private readonly ApplicationContext _context;

    public StatisticRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Song>> GetTopSongs(CancellationToken cancellationToken)
    {
        return await _context.Songs.OrderByDescending(s=> s.Views).Take(10).ToListAsync(cancellationToken);
        
    }

    public async Task<List<Song>> GetTopArtist(CancellationToken cancellationToken)
    {
        return await _context.Songs
        .GroupBy( s=> new {s.IdArtist,NameArtist = s.Artist.Name})
        .Select(g=> new
        {
            IdArtist = g.Key.IdArtist,
            NameArtist = g.Key.NameArtist,
            TotalSongs = g.Count(),
            TotalViews = g.Sum(s => s.Views)
            
        }).OrderByDescending(a => a.TotalViews)
        .Take(10)
        .Select(a => _context.Songs.First(s => s.IdArtist == a.IdArtist))
        .ToListAsync(cancellationToken);
        

        
        
    
        }


    public async Task<List<Statistic>> GetAll()
    {
        return await _context.Statistics.ToListAsync();
    }


    public async Task<Statistic> GetById(Guid Id, CancellationToken cancellationToken)
    {

        var statistic = await _context.Statistics.FirstAsync(s => s.Id == Id, cancellationToken);
        if (statistic == null)
        {
            throw new KeyNotFoundException($"La estadística con el ID {Id} no fue encontrada.");
        }
        return statistic;
    }


    public async Task<Statistic> Create(Statistic statistic, CancellationToken cancellationToken)
    {
        await _context.Statistics.AddAsync(statistic, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return statistic;
    }

    public async Task Delete(Guid Id, CancellationToken cancellationToken)
    {
        var statistic = await _context.Statistics.FindAsync(Id);
        if (statistic != null)
        {
            _context.Statistics.Remove(statistic);
        }
        await _context.SaveChangesAsync(cancellationToken);
    }



    public async Task<Statistic> Update(Statistic statistic, CancellationToken cancellationToken)
    {

        _context.Update(
            statistic
        );

        await _context.SaveChangesAsync(cancellationToken);
        return statistic;

    }





}