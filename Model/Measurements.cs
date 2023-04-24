using System.ComponentModel.DataAnnotations;

namespace Model;

public class Measurements
{
    [Key]
    public int Id { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double Co2 { get; set; }
    [DataType(DataType.Date)]
    public DateOnly Date { get; set; }
    [DataType(DataType.Time)]
    public TimeOnly Time { get; set; }
}