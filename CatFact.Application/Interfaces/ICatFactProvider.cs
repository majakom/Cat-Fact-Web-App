using CatFact.Domain.Entities;

namespace CatFact.Application.Interfaces;

public interface ICatFactProvider
{
    Task<CatFactEntity?> GetFactAsync();
}