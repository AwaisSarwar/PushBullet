using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Services.Notifications.Entities
{
    public class NotificationEntity
    {
        public string Username { get; set; }
        public string NoteTitle { get; set; }
        public string NoteText { get; set; }
    }
}
