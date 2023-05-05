using System.ComponentModel.DataAnnotations;

namespace Model;

public class TerrariumLimits
{
    [Key]
    public Guid Id { get; set; }
    public double TemperatureLimitMax { get; set; }
    public double TemperatureLimitMin { get; set; }
}