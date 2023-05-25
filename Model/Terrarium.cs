using System.ComponentModel.DataAnnotations;

namespace Model;

public class Terrarium
{
    [Key] public String name { get; set; }
    public TerrariumBoundaries terrariumBoundaries { get; set; }
    public TerrariumLimits terrariumLimits { get; set; }
}