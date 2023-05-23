using Microsoft.AspNetCore.Mvc;
using Model;
using ReptiMate_Cloud.Controllers;

namespace ReptiMate_Cloud.Services;

public interface IAnimalServiceRest
{
    Task<Animal> CreateAnimalAsync(Animal animal);
    Task<ICollection<Animal>> GetAllAnimalsAsync();
}