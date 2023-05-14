using System.ComponentModel.DataAnnotations;

namespace Model;

public class Terrarium
{
    [Key]
    public String name { get; set; }
    public TerrariumBoundaries terrariumBoundaries;
    public TerrariumLimits terrariumLimits;
    public Measurements measurements;
}