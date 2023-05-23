using Microsoft.AspNetCore.Mvc;
using Model;

namespace ReptiMate_Cloud.Services;

public interface IAnimalServiceRest
{
    Task<Animal> CreateAnimalAsync(Animal animal);
    Task DeleteAnimalAsync(string id);
}