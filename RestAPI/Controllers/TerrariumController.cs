using Microsoft.AspNetCore.Mvc;
using Model;
using ReptiMate_Cloud.Services;

namespace ReptiMate_Cloud.Controllers;

[ApiController]
[Route("[controller]")]
public class TerrariumController : ControllerBase
{
    private readonly ITerrariumServiceRest terrariumServiceRest;

    public TerrariumController(ITerrariumServiceRest terrariumServiceRest)
    {
        this.terrariumServiceRest = terrariumServiceRest;
    }

    [HttpPut("boundaries")]
    public async Task<ActionResult<TerrariumBoundaries>> CreateTerrariumBoundariesAsync([FromBody] TerrariumBoundaries terrariumBoundaries)
    {
        try
        {
            TerrariumBoundaries updatedTerrariumBoundaries = await terrariumServiceRest.UpdateTerrariumBoundariesAsync(terrariumBoundaries);
            return Ok(updatedTerrariumBoundaries);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("boundaries")]
    public async Task<ActionResult<TerrariumBoundaries>> GetTerrariumBoundariesAsync()
    {
        try
        {
            TerrariumBoundaries terrariumBoundaries = await terrariumServiceRest.GetTerrariumBoundariesAsync();
            return Ok(terrariumBoundaries);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}