using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository.DAO;

public class NotificationDAO : INotificationDAO
{
    private readonly DatabaseContext context;

    public NotificationDAO(DatabaseContext context)
    {
        this.context = context;
    }
    
    public async Task UpdateNotificationAsync(List<string> idList)
    {
        var ids = idList.Select(id => Guid.Parse(id));
        var notifications = await context.Notifications.Where(notification => ids.Contains(notification.Id)).ToListAsync();

        foreach (var notification in notifications)
        {
            notification.Status = true;
        }

        await context.SaveChangesAsync();
    }

    public async Task<ICollection<Notification>> GetNotificationsAsync()
    {
        List<Notification> notifications = await context.Notifications!.OrderBy(notification => notification.DateTime).ToListAsync();

        return notifications;
    }

    public async Task CreateNotificationAsync(Notification notification)
    {
        await context.Notifications!.AddAsync(notification);
        try
        {
            await context.SaveChangesAsync();
        }
        catch(Exception e)
        {
           Console.WriteLine("Exception" + e); 
        }
    }
}