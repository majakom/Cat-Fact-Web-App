using Moq;
using CatFact.Application.Interfaces;
using CatFact.Domain.Entities;

namespace CatFact.Tests.Application.Services;

public class CatFactExportServiceTests
{
    [Fact]
    public async Task GetAllAsJsonAsync_ShouldReturnSerializedFacts()
    {
        var facts = new List<CatFactEntity>
        {
            new CatFactEntity { Fact = "Cats sleep a lot", Length = 17 },
            new CatFactEntity { Fact = "Cats have 9 lives", Length = 18 }
        };

        var repoMock = new Mock<ICatFactRepository>();

        repoMock
            .Setup(x => x.GetAllFactsAsync())
            .ReturnsAsync(facts);

        var service = new ExportService(repoMock.Object);
        var result = await service.GetAllAsJsonAsync();

        Assert.NotNull(result);
        Assert.Contains("Cats sleep a lot", result);
        Assert.Contains("Cats have 9 lives", result);

        repoMock.Verify(x => x.GetAllFactsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllAsJsonAsync_ShouldReturnEmptyArray_WhenNoData()
    {
        var repoMock = new Mock<ICatFactRepository>();

        repoMock
            .Setup(x => x.GetAllFactsAsync())
            .ReturnsAsync(new List<CatFactEntity>());

        var service = new ExportService(repoMock.Object);

        var result = await service.GetAllAsJsonAsync();
        Assert.Equal("[]", result);
    }

    [Fact]
    public async Task GetAllAsJsonAsync_ShouldCallRepositoryOnce()
    {
        var repoMock = new Mock<ICatFactRepository>();

        repoMock
            .Setup(x => x.GetAllFactsAsync())
            .ReturnsAsync(new List<CatFactEntity>());

        var service = new ExportService(repoMock.Object);
        await service.GetAllAsJsonAsync();
        repoMock.Verify(x => x.GetAllFactsAsync(), Times.Once);
    }
}