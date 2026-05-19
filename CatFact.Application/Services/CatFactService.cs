using CatFact.Application.Interfaces;
using CatFact.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CatFact.Application.Services;

public class CatFactService : ICatFactService
{
    private readonly ICatFactProvider _catFactProvider;
    private readonly IFileStorageService _fileStorageService;
    private readonly ILogger<CatFactService> _logger;

    public CatFactService(
        ICatFactProvider catFactProvider,
        IFileStorageService fileStorageService,
        ILogger<CatFactService> logger)
    {
        _catFactProvider = catFactProvider;
        _fileStorageService = fileStorageService;
        _logger = logger;
    }

    public async Task<CatFactEntity?> GenerateFactAsync()
    {
        try
        {
            var fact = await _catFactProvider.GetFactAsync();

            if (fact == null)
            {
                _logger.LogWarning("Cat fact was null.");
                return null;
            }

            var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {fact.Fact}";

            await _fileStorageService.AppendLineAsync(line);

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