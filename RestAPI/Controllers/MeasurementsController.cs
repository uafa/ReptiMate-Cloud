using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using ReptiMate_Cloud.Services;

namespace ReptiMate_Cloud.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementsController : ControllerBase
{

    private readonly IMeasurementsServiceRest measurementsServiceRest;

    public MeasurementsController(IMeasurementsServiceRest measurementsServiceRest)
    {
        this.measurementsServiceRest = measurementsServiceRest;
    }

    [HttpGet("latest")]
    public async Task<ActionResult<Measurements>> getLatestMeasurement()
    {
        try
        {
            Measurements? measurements = await measurementsServiceRest.getLatestMeasurement();
            Console.WriteLine(measurements.Time);
            return Ok(measurements);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}