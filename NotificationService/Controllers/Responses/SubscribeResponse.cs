using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Controllers.Responses
{
    public class SubscribeResponse
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public DateTime CreationTime { get; set; }
        public int NumOfNotificationsPushed { get; set; }
    }
}
