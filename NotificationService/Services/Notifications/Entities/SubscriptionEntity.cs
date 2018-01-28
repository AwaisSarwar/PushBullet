using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Services.Notifications.Entities
{
    public class SubscriptionEntity
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public DateTime CreationTime { get; set; }
        public int TotalNotificationsPushed { get; set; }
    }
}
