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
    
    public async Task UpdateNotificationAsync(string id)
    {
        Notification? notification = await context.Notifications.FindAsync(id);
        if (notification == null)
        {
            throw new Exception($"Notification with {id} not found");
        }
        notification.Status = true;

        context.Notifications.Update(notification);
        await context.SaveChangesAsync();
    }

    public async Task<ICollection<Notification>> GetNotificationsAsync()
    {
        List<Notification> notifications = context.Notifications.ToList();

        return notifications;
    }

    public async Task CreateNotificationAsync(Notification notification)
    {
        await context.Notifications.AddAsync(notification);
        await context.SaveChangesAsync();
    }
}