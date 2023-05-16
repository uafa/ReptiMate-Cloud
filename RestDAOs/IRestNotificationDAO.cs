using Model;

namespace RestDAOs;

public interface IRestNotificationDAO
{
    public Task UpdateNotificationAsync(List<string> idList);
    public Task<ICollection<Notification>> GetNotificationsAsync();
    public Task CreateNotificationAsync(Notification notification);
}