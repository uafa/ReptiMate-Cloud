using Model;

namespace WSDAOs;

public interface IWSNotificationDAO
{
    public Task CreateNotificationAsync(Notification notification);
}