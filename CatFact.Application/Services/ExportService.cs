using System.Text.Json;
using CatFact.Application.Interfaces;

public class ExportService : IExportService
{
    private readonly ICatFactRepository _repository;

    public ExportService(ICatFactRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> GetAllAsJsonAsync()
    {
        var facts = await _repository.GetAllFactsAsync();
        return JsonSerializer.Serialize(facts, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}