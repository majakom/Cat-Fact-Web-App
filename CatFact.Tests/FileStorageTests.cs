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

        provider.Setup(x => x.GetFactAsync())
            .ReturnsAsync(fact);

        var file = new Mock<IFileStorageService>();

        file.Setup(x => x.AppendLineAsync(It.IsAny<string>()))
            .Callback<string>(x => captured = x)
            .Returns(Task.CompletedTask);

        var repository = new Mock<ICatFactRepository>();

        repository.Setup(x => x.AddFactAsync(It.IsAny<CatFactEntity>()))
            .Returns(Task.CompletedTask);

        var logger = new Mock<ILogger<CatFactService>>();

        var service = new CatFactService(
            provider.Object,
            file.Object,
            repository.Object,
            logger.Object
        );

        await service.GenerateFactAsync();

        Assert.NotNull(captured);

        Assert.Contains(fact.Fact, captured);
        Assert.Contains(fact.Length.ToString(), captured);

        file.Verify(
            x => x.AppendLineAsync(It.IsAny<string>()),
            Times.Once
        );

        repository.Verify(
            x => x.AddFactAsync(It.IsAny<CatFactEntity>()),
            Times.Once
        );
    }
}