using CatFact.Application.Interfaces;
using CatFact.Domain.Entities;
using CatFact.Infrastructure.Mappers;
using CatFact.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CatFact.Infrastructure.Repositories;

public class CatFactRepository : ICatFactRepository
{
    private readonly CatFactDbContext _context;
    public CatFactRepository(CatFactDbContext context)
    {
        _context = context;
    }

    public async Task AddFactAsync(CatFactEntity fact)
    {
        var dbEntity = CatFactMapper.ToDb(fact);
        await _context.CatFacts.AddAsync(dbEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CatFactEntity>> GetAllFactsAsync()
    {
        var data = await _context.CatFacts.ToListAsync();
        return data.Select(CatFactMapper.ToDomain).ToList();
    }
}