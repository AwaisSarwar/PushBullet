using NotificationService.Services.Notifications.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Services.Notifications
{
    public interface INotificationsService
    {
        SubscriptionEntity Subscribe(SubscriptionEntity entity);
        List<SubscriptionEntity> GetSubscriptions();
        Task<NotificationStatusEntity> SendNotification(NotificationEntity entity);
    }
}
