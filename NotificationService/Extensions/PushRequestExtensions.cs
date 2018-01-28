using NotificationService.Controllers.Requests;
using NotificationService.Controllers.Responses;
using NotificationService.Repositories.DataModels;
using NotificationService.Services.Notifications.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Extensions
{
    public static class PushRequestExtensions
    {
        public static NotificationEntity ToNotificationEntity(this PushRequest request)
        {
            return new NotificationEntity
            {
                Username = request.Username,
                NoteText = request.Text,
                NoteTitle = request.Title
            };
        }
    }
}
