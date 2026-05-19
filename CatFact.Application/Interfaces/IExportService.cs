namespace CatFact.Application.Interfaces;

public interface IExportService
{
    Task<string> GetAllAsJsonAsync();
}