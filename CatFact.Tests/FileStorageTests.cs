using Xunit;
using Moq;
using CatFact.Application.Services;
using CatFact.Application.Interfaces;
using CatFact.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CatFact.Tests.Application.Services;

public class FileStorageTests
{
    [Fact]
    public async Task ShouldWriteFormattedLineToFile()
    {
        var fact = new CatFactEntity
        {
            Fact = "Cats sleep a lot",
            Length = 17
        };

        string? captured = null;

        var provider = new Mock<ICatFactProvider>();
        provider.Setup(x => x.GetFactAsync()).ReturnsAsync(fact);

        var file = new Mock<IFileStorageService>();
        file.Setup(x => x.AppendLineAsync(It.IsAny<string>()))
            .Callback<string>(x => captured = x)
            .Returns(Task.CompletedTask);

        var logger = new Mock<ILogger<CatFactService>>();

        var service = new CatFactService(provider.Object, file.Object, logger.Object);

        await service.GenerateFactAsync();

        Assert.NotNull(captured);
        Assert.Contains("Cats sleep a lot", captured);
    }
}