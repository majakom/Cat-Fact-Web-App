using CatFact.Domain.Entities;
using CatFact.Infrastructure.Persistence.Models;

namespace CatFact.Infrastructure.Mappers;

public static class CatFactMapper
{
    public static CatFactDbEntity ToDb(CatFactEntity domain)
    {
        return new CatFactDbEntity
        {
            Fact = domain.Fact,
            Length = domain.Length
        };
    }

    public static CatFactEntity ToDomain(CatFactDbEntity db)
    {
        return new CatFactEntity
        {
            Fact = db.Fact,
            Length = db.Length
        };
    }
}