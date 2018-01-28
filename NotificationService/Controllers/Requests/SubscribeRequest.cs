using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Controllers.Requests
{
    public class SubscribeRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string AccessToken { get; set; }
    }
}
