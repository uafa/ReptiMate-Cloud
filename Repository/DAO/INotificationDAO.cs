using Model;

namespace Repository.DAO;

public interface INotificationDAO
{
    public Task UpdateNotificationAsync(string id);
    public Task<ICollection<Notification>> GetNotificationsAsync();
    public Task CreateNotificationAsync(Notification notification);
}