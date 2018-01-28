using NotificationService.Controllers.Responses;
using NotificationService.Services.Notifications.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Extensions
{
    public static class NotificationStatusEntityExtensions
    {
        public static NotificationStatusResponse ToNotificationStatusResponse(this NotificationStatusEntity entity)
        {
            return new NotificationStatusResponse
            {
                IsSent = entity.IsSent,
                Status = entity.StatusText
            };
        }
    }
}
