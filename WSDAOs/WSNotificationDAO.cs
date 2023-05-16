using Model;
using Repository;

namespace WSDAOs;

public class WSNotificationDAO : IWSNotificationDAO
{
    private readonly DatabaseContext context;

    public WSNotificationDAO(DatabaseContext context)
    {
        this.context = context;
    }

    public async Task CreateNotificationAsync(Notification notification)
    {
        await context.Notifications!.AddAsync(notification);
        await context.SaveChangesAsync();
    }
}