﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using ReptiMate_Cloud.Services;

namespace ReptiMate_Cloud.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
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
    
    [HttpPut("limits")]
    public async Task<ActionResult<TerrariumBoundaries>> CreateTerrariumBoundariesAsync([FromBody] TerrariumLimits terrariumLimits)
    {
        try
        {
            var createdTerrariumLimits =
                await terrariumServiceRest.UpdateTerrariumLimitsAsync(terrariumLimits);
            return Ok(createdTerrariumLimits);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("limits")]
    public async Task<ActionResult<TerrariumLimits>> GetTerrariumLimitsAsync()
    {
        try
        {
            var terrariumLimits = await terrariumServiceRest.GetTerrariumLimitsAsync();
            return Ok(terrariumLimits);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    [HttpGet]
    public async Task<ActionResult<Terrarium>> GetTerrariumAsync()
    {
        try
        {
            var terrarium = await terrariumServiceRest.GetTerrariumAsync();
            return Ok(terrarium);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
}