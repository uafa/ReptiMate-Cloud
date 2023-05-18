using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using ReptiMate_Cloud.Services;

namespace ReptiMate_Cloud.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
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
    public async Task<ActionResult<Measurements>> GetLatestMeasurement()
    {
        try
        {
            Measurements measurements = await measurementsServiceRest.GetLatestMeasurementAsync();
            return Ok(measurements);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Measurements>>> GetAllMeasurementsAsync([FromQuery] string? dateFrom,
        [FromQuery] string? dateTo)
    {
        try
        {
            var dateTimeFrom = new DateTime();
            var dateTimeTo = new DateTime();
            if (!String.IsNullOrEmpty(dateFrom) && !String.IsNullOrEmpty(dateTo))
            {
                dateTimeFrom = DateTime.Parse(dateFrom);
                dateTimeTo = DateTime.Parse(dateTo);
            }

            var measurements = await measurementsServiceRest.GetAllMeasurementsAsync(dateTimeFrom, dateTimeTo);
            return Ok(measurements);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}