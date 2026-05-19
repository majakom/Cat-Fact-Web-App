using System.Text.Json;
using CatFact.Domain.Entities;
using CatFact.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace CatFact.Infrastructure.External;

public class CatFactApiClient : ICatFactProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CatFactApiClient> _logger;

    public CatFactApiClient(
        HttpClient httpClient,
        ILogger<CatFactApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<CatFactEntity?> GetFactAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://catfact.ninja/fact");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var fact = JsonSerializer.Deserialize<CatFactEntity>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return fact;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while fetching cat fact.");
            return null;
        }
    }
}