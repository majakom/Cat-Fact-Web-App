using Moq;
using Xunit;
using CatFact.Application.Services;
using CatFact.Application.Interfaces;
using CatFact.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CatFact.Tests.Application.Services;

public class ErrorHandlingTests
{
    [Fact]
    public async Task ShouldReturnNull_WhenProviderReturnsNull()
    {
        var provider = new Mock<ICatFactProvider>();

        provider.Setup(x => x.GetFactAsync())
            .ReturnsAsync((CatFactEntity?)null);

        var file = new Mock<IFileStorageService>();

        var repository = new Mock<ICatFactRepository>();

        var logger = new Mock<ILogger<CatFactService>>();

        var service = new CatFactService(
            provider.Object,
            file.Object,
            repository.Object,
            logger.Object
        );

        var result = await service.GenerateFactAsync();

        Assert.Null(result);

        file.Verify(
            x => x.AppendLineAsync(It.IsAny<string>()),
            Times.Never
        );

        repository.Verify(
            x => x.AddFactAsync(It.IsAny<CatFactEntity>()),
            Times.Never
        );
    }

    [Fact]
    public async Task ShouldReturnNull_WhenExceptionThrown()
    {
        var provider = new Mock<ICatFactProvider>();

        provider.Setup(x => x.GetFactAsync())
            .ThrowsAsync(new Exception("API error"));

        var file = new Mock<IFileStorageService>();
        var repository = new Mock<ICatFactRepository>();
        var logger = new Mock<ILogger<CatFactService>>();
        var service = new CatFactService(
            provider.Object,
            file.Object,
            repository.Object,
            logger.Object
        );

        var result = await service.GenerateFactAsync();

        Assert.Null(result);
        file.Verify(
            x => x.AppendLineAsync(It.IsAny<string>()),
            Times.Never
        );
        repository.Verify(
            x => x.AddFactAsync(It.IsAny<CatFactEntity>()),
            Times.Never
        );
    }
}