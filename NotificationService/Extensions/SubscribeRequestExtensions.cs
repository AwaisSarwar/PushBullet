using NotificationService.Controllers.Requests;
using NotificationService.Services.Notifications.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Extensions
{
    public static class SubscribeRequestExtensions
    {
        public static SubscriptionEntity ToSubscriptionEntity(this SubscribeRequest request)
        {
            return new SubscriptionEntity
            {
                AccessToken = request.AccessToken,
                Username = request.Username
            };
        }
    }
}
