using System.ComponentModel.DataAnnotations;

namespace Model;

public class Account
{
    [Key]
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}