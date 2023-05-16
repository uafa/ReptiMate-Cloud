using Model;

namespace RestDAOs;

public interface IRestNotificationDAO
{
    public Task UpdateNotificationAsync(string id);
    public Task<ICollection<Notification>> GetNotificationsAsync();
    public Task CreateNotificationAsync(Notification notification);
}