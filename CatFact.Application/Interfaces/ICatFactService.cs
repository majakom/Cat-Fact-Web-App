using CatFact.Domain.Entities;

namespace CatFact.Application.Interfaces;

public interface ICatFactService
{
    Task<CatFactEntity?> GenerateFactAsync();
}