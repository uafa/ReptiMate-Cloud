using System.ComponentModel.DataAnnotations;

namespace Model;

public class Notification
{
    [Key] 
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Message { get; set; }
    public bool Status { get; set; } // false = unread, true = read
    
}