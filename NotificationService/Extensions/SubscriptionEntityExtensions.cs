using NotificationService.Controllers.Responses;
using NotificationService.Repositories.DataModels;
using NotificationService.Services.Notifications.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Extensions
{
    public static class SubscriptionEntityExtensions
    {
        public static SubscribeResponse ToSubscribeResponse(this SubscriptionEntity entity)
        {
            return new SubscribeResponse
            {
                AccessToken = entity.AccessToken,
                CreationTime = entity.CreationTime,
                NumOfNotificationsPushed = entity.TotalNotificationsPushed,
                Username = entity.Username
            };
        }

        public static SubscriptionDataModel ToSubscriptionDataModel(this SubscriptionEntity entity)
        {
            return new SubscriptionDataModel
            {
                AccessToken = entity.AccessToken,
                CreationTime = entity.CreationTime,
                TotalNotificationsPushed = entity.TotalNotificationsPushed,
                Username = entity.Username
            };
        }

        public static SubscriptionResponse ToSubscriptionResponse(this SubscriptionEntity entity)
        {
            return new SubscriptionResponse
            {
                AccessToken = entity.AccessToken,
                CreationTime = entity.CreationTime,
                NumOfNotificationsPushed = entity.TotalNotificationsPushed,
                Username = entity.Username
            };
        }

        public static List<SubscriptionResponse> ToSubscriptionResponses(this List<SubscriptionEntity> entities)
        {
            var responses = new List<SubscriptionResponse>();

            entities.ForEach(_ => responses.Add(_.ToSubscriptionResponse()));

            return responses;
        }
    }
}
