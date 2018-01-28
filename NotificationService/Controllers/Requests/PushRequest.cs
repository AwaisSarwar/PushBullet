using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Controllers.Requests
{
    public class PushRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
