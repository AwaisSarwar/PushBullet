using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Controllers.Responses
{
    public class NotificationStatusResponse
    {
        public bool IsSent { get; set; }
        public string Status { get; set; }
    }
}
