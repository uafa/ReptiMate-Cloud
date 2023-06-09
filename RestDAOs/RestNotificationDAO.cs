using Microsoft.EntityFrameworkCore;
using Model;
using Repository;

namespace RestDAOs;

public class RestNotificationDAO : IRestNotificationDAO
{
    private readonly DatabaseContext context;

    public RestNotificationDAO(DatabaseContext context)
    {
        this.context = context;
    }
    
    public async Task UpdateNotificationAsync(List<Guid> ids)
    {
        var notifications = await context.Notifications!.Where(notification => ids.Contains(notification.Id)).ToListAsync();

        foreach (var notification in notifications)
        {
            notification.Status = true;
        }
        await context.SaveChangesAsync();
    }

    public async Task<ICollection<Notification>> GetNotificationsAsync()
    {
        List<Notification> notifications = 
            await context.Notifications!.OrderByDescending(notification => notification.DateTime)
                .ToListAsync();

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