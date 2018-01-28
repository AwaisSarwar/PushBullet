using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Services.Notifications;
using NotificationService.Controllers.Requests;
using NotificationService.Extensions;
using NotificationService.Controllers.Responses;
using System.Threading.Tasks;

namespace NotificationService.Controllers
{
    [Route("api/[controller]")]
    public class NotificationsController : Controller
    {
        private INotificationsService _notificationsService;
        public NotificationsController(INotificationsService notificationsService)
        {
            _notificationsService = notificationsService;
        }

        // POST api/notifications
        [HttpPost]
        public ActionResult Post([FromBody]SubscribeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            var subscription = _notificationsService.Subscribe(request.ToSubscriptionEntity());
            if (subscription.CreationTime == DateTime.MinValue)
            {
                return new JsonResult(new { status = "Failed", message = "Failed to add subscription" });
            }

            return new JsonResult(subscription.ToSubscribeResponse());

        }

        // GET api/notifications
        [HttpGet]
        public List<SubscriptionResponse> Get()
        {
            return _notificationsService.GetSubscriptions().ToSubscriptionResponses();
        }

        // PUT api/notifications
        [HttpPut]
        public async Task<ActionResult> Put([FromBody]PushRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            var result = await _notificationsService.SendNotification(request.ToNotificationEntity());

            return new JsonResult(result.ToNotificationStatusResponse());
        }
    }
}
