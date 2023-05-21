using Model;
using Repository;

namespace RestDAOs;

public class RestAnimalDAO : IRestAnimalDAO
{
    private readonly DatabaseContext context;

    public RestAnimalDAO(DatabaseContext context)
    {
        this.context = context;
    }

    public async Task CreateAnimalAsync(Animal animal)
    {
        await context.Animals.AddAsync(animal);
        await context.SaveChangesAsync();
    }
}