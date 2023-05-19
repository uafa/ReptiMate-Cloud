using Model;
using Repository;

namespace WSDAOs;

public class WSNotificationsDAO : IWSNotificationDAO
{
    private readonly DatabaseContext context;

    public WSNotificationsDAO(DatabaseContext context)
    {
        this.context = context;
    }

    public async Task CreateNotificationAsync(Notification notification)
    {
        await context.Notifications!.AddAsync(notification);
        await context.SaveChangesAsync();
    }
}