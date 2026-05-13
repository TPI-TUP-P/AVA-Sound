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

    public async Task<List<Statistic>> GetAll()
    {
        return await _context.Statistics.ToListAsync();
    }


    public async Task<Statistic> GetById(Guid Id)
    {

        var statistic = await _context.Statistics.FirstAsync(s => s.Id == Id);
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

    public async Task Delete(Guid Id)
    {
        var statistic = await _context.Statistics.FindAsync(Id);
        if (statistic != null)
        {
            _context.Statistics.Remove(statistic);
        }
        await _context.SaveChangesAsync();
    }



    public async Task<Statistic> Update(Statistic statistic)
    {

        _context.Update(
            statistic
        );

        await _context.SaveChangesAsync();
        return statistic;

    }





}