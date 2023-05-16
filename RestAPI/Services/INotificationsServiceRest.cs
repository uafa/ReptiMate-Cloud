using Microsoft.AspNetCore.Mvc;
using Model;

namespace ReptiMate_Cloud.Services;

public interface INotificationsService
{
    Task UpdateNotificationAsync(List<string> ids);
    Task<ICollection<Notification>> GetAllNotificationsAsync();
}