using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationService.Repositories.DataModels;
using NotificationService.Repositories.Exceptions;

namespace NotificationService.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private object _lock = new object();
        private List<SubscriptionDataModel> _subscriptions = new List<SubscriptionDataModel>();

        public SubscriptionDataModel Add(SubscriptionDataModel subscription)
        {
            if (subscription == null) return subscription;

            lock(_lock)
            {
                if (_subscriptions.Any(_ => _.Username == subscription.Username))
                {
                    throw new DuplicateSubscriptionException();
                }

                subscription.CreationTime = DateTime.Now;
                subscription.TotalNotificationsPushed = 0;
                _subscriptions.Add(subscription);
            }

            return subscription;
        }

        public List<SubscriptionDataModel> GetAll()
        {
            return _subscriptions.ToList();
        }

        public SubscriptionDataModel GetSubscription(string username)
        {
            return _subscriptions.FirstOrDefault(_ => _.Username == username);
        }

        public bool Remove(SubscriptionDataModel subscription)
        {
            throw new NotImplementedException();
        }

        public bool Update(SubscriptionDataModel subscription)
        {
            var toUpdate = _subscriptions.FirstOrDefault(_ => _.Username == subscription.Username);
            if (toUpdate == null)
                return false;

            toUpdate.TotalNotificationsPushed = subscription.TotalNotificationsPushed;
            return true;
        }
    }
}
