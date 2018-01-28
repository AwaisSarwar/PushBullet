using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationService.Services.Notifications.Entities;
using NotificationService.Repositories;
using NotificationService.Extensions;
using NotificationService.Repositories.Exceptions;
using System.Net.Http;
using NotificationService.Helpers;
using Newtonsoft.Json;
using System.Text;

namespace NotificationService.Services.Notifications
{
    public class PushBulletNotificationsService : INotificationsService
    {
        private ISubscriptionRepository _repository;
        private const string _pushUrl = "https://api.pushbullet.com/v2/pushes";
        private IHttpClientWrapper _httpClient; 

        public PushBulletNotificationsService(ISubscriptionRepository repository, IHttpClientWrapper httpClient)
        {
            _repository = repository;
            _httpClient = httpClient;
        }

        public SubscriptionEntity Subscribe(SubscriptionEntity entity)
        {
            try
            {
                var subscriptionDataModel = entity.ToSubscriptionDataModel();
                var result = _repository.Add(subscriptionDataModel);
                if (result != null)
                {
                    return result.ToSubscriptionEntity();
                }
            }
            catch (DuplicateSubscriptionException)
            {
                return entity;
                // May be log?
            }

            // Uninitialized entity returned to indicate failure
            return entity;
        }

        public List<SubscriptionEntity> GetSubscriptions()
        {
            return _repository.GetAll().ToSubscriptionEntities();
        }

        public async Task<NotificationStatusEntity> SendNotification(NotificationEntity entity)
        {
            // Get user access tokens
            var subscriptionEntity = _repository.GetSubscription(entity.Username).ToSubscriptionEntity();

            // Send notification
            var headers = new Dictionary<string, string>();

            var content = JsonConvert.SerializeObject(new { body = entity.NoteText, title = entity.NoteTitle, type = "note" });
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            stringContent.Headers.Add("Access-Token", subscriptionEntity.AccessToken);

            var response = await _httpClient.PostAsync(_pushUrl, null, stringContent);
            if (!response.IsSuccessStatusCode)
            {
                return new NotificationStatusEntity { IsSent = false, StatusText = "Unable to send notification" };                    
            }
            subscriptionEntity.TotalNotificationsPushed++;
            

            // Update subscription
            _repository.Update(subscriptionEntity.ToSubscriptionDataModel());

            return new NotificationStatusEntity { IsSent = true, StatusText = "Success" };
        }
    }
}
