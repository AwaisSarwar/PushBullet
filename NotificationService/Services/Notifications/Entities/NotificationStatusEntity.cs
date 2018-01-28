using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Services.Notifications.Entities
{
    public class NotificationStatusEntity
    {
        public bool IsSent { get; set; }
        public string StatusText { get; set; }
    }
}
