using Microsoft.AspNetCore.Mvc;
using Model;
using RestDAOs;

namespace ReptiMate_Cloud.Services;

public class AnimalServiceRest : IAnimalServiceRest
{
    private IRestAnimalDAO animalDao;

    public AnimalServiceRest(IRestAnimalDAO animalDao)
    {
        this.animalDao = animalDao;
    }
    public async Task<Animal> CreateAnimalAsync(Animal animal)
    {
        await animalDao.CreateAnimalAsync(animal);
        return animal;
    }

    public async Task<ICollection<Animal>> GetAllAnimalsAsync()
    {
        return await animalDao.GetAllAnimalsAsync();
    }
}