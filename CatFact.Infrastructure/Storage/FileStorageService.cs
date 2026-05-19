using CatFact.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace CatFact.Infrastructure.Storage;

public class FileStorageService : IFileStorageService
{
    private readonly ILogger<FileStorageService> _logger;

    private readonly string _filePath = "facts.txt";

    public FileStorageService(ILogger<FileStorageService> logger)
    {
        _logger = logger;
    }

    public async Task AppendLineAsync(string content)
    {
        try
        {
            await File.AppendAllTextAsync(
                _filePath,
                content + Environment.NewLine
            );

            _logger.LogInformation("Line appended to file.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while writing to file.");
            throw;
        }
    }
}