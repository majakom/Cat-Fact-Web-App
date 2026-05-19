using Xunit;
using Moq;
using CatFact.Application.Services;
using CatFact.Application.Interfaces;
using CatFact.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CatFact.Tests.Application.Services;

public class ProviderTests
{
    [Fact]
    public async Task ShouldCallProviderExactlyOnce()
    {
        var provider = new Mock<ICatFactProvider>();
        provider.Setup(x => x.GetFactAsync())
            .ReturnsAsync(new CatFactEntity { Fact = "test", Length = 4 });

        var file = new Mock<IFileStorageService>();
        var logger = new Mock<ILogger<CatFactService>>();

        var service = new CatFactService(provider.Object, file.Object, logger.Object);

        await service.GenerateFactAsync();

        provider.Verify(x => x.GetFactAsync(), Times.Once);
    }
}