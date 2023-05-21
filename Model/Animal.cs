using System.ComponentModel.DataAnnotations;

namespace Model;

public class Animal
{
    [Key] 
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public char Gender { get; set; }
    public string Color { get; set; }
    public string Species { get; set; }
}