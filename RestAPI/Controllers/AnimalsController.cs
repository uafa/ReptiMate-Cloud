using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using ReptiMate_Cloud.Services;

namespace ReptiMate_Cloud.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalServiceRest animalServiceRest;
    
    public AnimalsController(IAnimalServiceRest animalServiceRest)
    {
        this.animalServiceRest = animalServiceRest;
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateAnimalAsync(Animal animal)
    {
        try
        {
            var animalCreated = await animalServiceRest.CreateAnimalAsync(animal);
            return Ok(animalCreated);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}