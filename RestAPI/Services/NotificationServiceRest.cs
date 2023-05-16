using Microsoft.AspNetCore.Mvc;
using Model;
using Repository.DAO;

namespace ReptiMate_Cloud.Services;

public class NotificationServiceRest : INotificationsService
{
    private INotificationDAO notificationDao;

    public NotificationServiceRest(INotificationDAO notificationDao)
    {
        this.notificationDao = notificationDao;
    }
    
    public async Task UpdateNotificationAsync(string id)
    {
        await notificationDao.UpdateNotificationAsync(id);
    }

    public async Task<ICollection<Notification>> GetAllNotificationsAsync()
    {
        var notifications = await notificationDao.GetNotificationsAsync();

        if (notifications == null)
        {
            throw new Exception("Notifications not found");
        }

        return notifications;
    }
}