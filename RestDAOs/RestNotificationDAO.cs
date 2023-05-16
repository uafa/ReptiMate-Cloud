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
    
    public async Task UpdateNotificationAsync(string id)
    {
        Notification? notification = await context.Notifications!.FindAsync(Guid.Parse(id));
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
        List<Notification> notifications = context.Notifications!.ToList();

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