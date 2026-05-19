namespace CatFact.Application.Interfaces;

public interface IFileStorageService
{
    Task AppendLineAsync(string content);
}