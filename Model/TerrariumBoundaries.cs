using System.ComponentModel.DataAnnotations;

namespace Model;

public class TerrariumBoundaries
{
    [Key]
    public Guid Id { get; set; }
    private double TemperatureBoundaryMax { get; set; }
    private double TemperatureBoundaryMin { get; set; }
    private double HumidityBoundaryMax { get; set; }
    private double HumidityBoundaryMin { get; set; }
    private double CO2BoundaryMax { get; set; }
    private double CO2BoundaryMin { get; set; }
}