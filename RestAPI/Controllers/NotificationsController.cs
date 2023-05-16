using Microsoft.AspNetCore.Mvc;
using Model;
using ReptiMate_Cloud.Services;

namespace ReptiMate_Cloud.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationsService notificationService;

    public NotificationsController(INotificationsService notificationService)
    {
        this.notificationService = notificationService;
    }

    [HttpPut]
    public async Task<IActionResult> UpdateNotification([FromQuery]string id)
    {
        try
        {
            await notificationService.UpdateNotificationAsync(id);
            Console.WriteLine("heeereeeeeee");
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Notification>>> GetAllNotifications()
    {
        try
        {
            ICollection<Notification> notifications = await notificationService.GetAllNotificationsAsync();
            return Ok(notifications);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}