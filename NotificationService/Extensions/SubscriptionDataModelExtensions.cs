using NotificationService.Controllers.Responses;
using NotificationService.Repositories.DataModels;
using NotificationService.Services.Notifications.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Extensions
{
    public static class SubscriptionDataModelExtensions
    {
        public static SubscriptionEntity ToSubscriptionEntity(this SubscriptionDataModel model)
        {
            return new SubscriptionEntity
            {
                AccessToken = model.AccessToken,
                CreationTime = model.CreationTime,
                TotalNotificationsPushed = model.TotalNotificationsPushed,
                Username = model.Username
            };
        }

        public static List<SubscriptionEntity> ToSubscriptionEntities(this List<SubscriptionDataModel> models)
        {
            var entities = new List<SubscriptionEntity>();

            models.ForEach(_ => entities.Add(_.ToSubscriptionEntity()));

            return entities;
        }
    }
}
