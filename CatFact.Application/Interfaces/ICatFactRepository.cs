using CatFact.Domain.Entities;

namespace CatFact.Application.Interfaces;

public interface ICatFactRepository
{
    Task AddFactAsync(CatFactEntity fact);
    Task<List<CatFactEntity>> GetAllFactsAsync();
}