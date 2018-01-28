using NotificationService.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Repositories
{
    public interface ISubscriptionRepository
    {
        SubscriptionDataModel Add(SubscriptionDataModel subscription);
        List<SubscriptionDataModel> GetAll();
        SubscriptionDataModel GetSubscription(string username);
        bool Remove(SubscriptionDataModel subscription);
        bool Update(SubscriptionDataModel subscription);
    }
}
