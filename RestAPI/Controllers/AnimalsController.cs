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

    [HttpGet]
    public async Task<ActionResult<ICollection<Animal>>> GetAllAnimalsAsync()
    {
        try
        {
            var animals = await animalServiceRest.GetAllAnimalsAsync();
            return Ok(animals);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
     }
     
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAnimalAsync([FromRoute] string id)
    {
        try
        {
            await animalServiceRest.DeleteAnimalAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}