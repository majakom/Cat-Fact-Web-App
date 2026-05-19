using CatFact.Application.Interfaces;
using CatFact.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CatFact.Application.Services;

public class CatFactService : ICatFactService
{
    private readonly ICatFactProvider _catFactProvider;
    private readonly IFileStorageService _fileStorageService;
    private readonly ICatFactRepository _repository;
    private readonly ILogger<CatFactService> _logger;

    public CatFactService(
        ICatFactProvider catFactProvider,
        IFileStorageService fileStorageService,
        ICatFactRepository repository,
        ILogger<CatFactService> logger)
    {
        _catFactProvider = catFactProvider;
        _fileStorageService = fileStorageService;
        _repository = repository;
        _logger = logger;
    }

    public async Task<CatFactEntity?> GenerateFactAsync()
    {
        try
        {
            var fact = await _catFactProvider.GetFactAsync();
            if (fact == null || string.IsNullOrWhiteSpace(fact.Fact))
            {
                _logger.LogWarning("Invalid cat fact received");
                return null;
            }
            var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {fact.Fact} | length: {fact.Length}";
            await _fileStorageService.AppendLineAsync(line);
            await _repository.AddFactAsync(fact);
            _logger.LogInformation("Cat fact successfully saved.");
            return fact;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while generating cat fact.");
            return null;
        }
    }
}