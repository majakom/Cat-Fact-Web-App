using Moq;
using CatFact.Application.Services;
using CatFact.Application.Interfaces;
using CatFact.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CatFact.Tests.Application.Services;

public class CatFactServiceTests
{
    [Fact]
    public async Task GenerateFactAsync_ShouldReturnFact_AndSaveToFile()
    {
        var expectedFact = new CatFactEntity
        {
            Fact = "Cats sleep most of the day.",
            Length = 27
        };

        var providerMock = new Mock<ICatFactProvider>();
        providerMock
            .Setup(x => x.GetFactAsync())
            .ReturnsAsync(expectedFact);

        var fileMock = new Mock<IFileStorageService>();
        fileMock
            .Setup(x => x.AppendLineAsync(It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        var loggerMock = new Mock<ILogger<CatFactService>>();

        var service = new CatFactService(
            providerMock.Object,
            fileMock.Object,
            loggerMock.Object
        );

        var result = await service.GenerateFactAsync();
        Assert.NotNull(result);
        Assert.Equal(expectedFact.Fact, result!.Fact);
        Assert.Equal(expectedFact.Length, result.Length);

        fileMock.Verify(x => x.AppendLineAsync(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task GenerateFactAsync_ShouldReturnNull_WhenApiFails()
    {
        var providerMock = new Mock<ICatFactProvider>();
        providerMock
            .Setup(x => x.GetFactAsync())
            .ReturnsAsync((CatFactEntity?)null);

        var fileMock = new Mock<IFileStorageService>();
        var loggerMock = new Mock<ILogger<CatFactService>>();

        var service = new CatFactService(
            providerMock.Object,
            fileMock.Object,
            loggerMock.Object
        );

        var result = await service.GenerateFactAsync();
        Assert.Null(result);
        fileMock.Verify(x => x.AppendLineAsync(It.IsAny<string>()), Times.Never);
    }
}