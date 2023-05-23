using Model;

namespace RestDAOs;

public interface IRestAnimalDAO
{
    Task CreateAnimalAsync(Animal animal);
    Task<ICollection<Animal>> GetAllAnimalsAsync();
    Task DeleteAnimalAsync(string id);
}