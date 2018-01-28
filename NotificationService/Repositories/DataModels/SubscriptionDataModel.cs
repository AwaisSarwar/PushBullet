using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Repositories.DataModels
{
    public class SubscriptionDataModel
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public DateTime CreationTime { get; set; }
        public int TotalNotificationsPushed { get; set; }
    }
}
