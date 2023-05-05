using System.ComponentModel.DataAnnotations;

namespace Model;

public class TerrariumBoundaries
{
    [Key] public Guid Id { get; set; }
    public double TemperatureBoundaryMax { get; set; }
    public double TemperatureBoundaryMin { get; set; }
    public double HumidityBoundaryMax { get; set; }
    public double HumidityBoundaryMin { get; set; }
    public double CO2BoundaryMax { get; set; }
    public double CO2BoundaryMin { get; set; }
}