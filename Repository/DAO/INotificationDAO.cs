using Model;

namespace Repository.DAO;

public interface INotificationDAO
{
    public Task UpdateNotificationAsync(List<string> idList);
    public Task<ICollection<Notification>> GetNotificationsAsync();
    public Task CreateNotificationAsync(Notification notification);
}