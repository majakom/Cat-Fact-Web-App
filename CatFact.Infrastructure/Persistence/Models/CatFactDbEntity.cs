namespace CatFact.Infrastructure.Persistence.Models;

public class CatFactDbEntity
{
    public int Id { get; set; }
    public string Fact { get; set; } = string.Empty;
    public int Length { get; set; }
}