using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotificationService.Repositories.DataModels;
using NotificationService.Repositories;
using Moq;
using NotificationService.Repositories.Exceptions;
using NotificationService.Services.Notifications.Entities;
using NotificationService.Services.Notifications;
using NotificationService.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class PushBulletNotificationsServiceTests
    {
        private Mock<ISubscriptionRepository> _repository;
        private Mock<IHttpClientWrapper> _client;
        private INotificationsService _service;

        public PushBulletNotificationsServiceTests()
        {
            _repository = new Mock<ISubscriptionRepository>();
            _client = new Mock<IHttpClientWrapper>();
            _service = new PushBulletNotificationsService(_repository.Object, _client.Object);
        }

        [TestMethod]
        public void When_SubscribeIsCalled_Then_SubscriptionIsAdded()
        {   
            // Setup
            var subscription = new SubscriptionEntity
            {
                Username = "bbc",
                AccessToken = "testtoken"
            };

            _repository.Setup(_ => _.Add(It.IsAny<SubscriptionDataModel>())).Returns(new SubscriptionDataModel
            {
                Username = "bbc",
                AccessToken = "testtoken",
                TotalNotificationsPushed = 0,
                CreationTime = DateTime.Now
            });

            var result = _service.Subscribe(subscription);

            // Assert
            Assert.AreEqual(0, result.TotalNotificationsPushed);
            Assert.AreEqual(DateTime.Now.ToString("dd MM yyyy"), result.CreationTime.ToString("dd MM yyyy"));
        }

        [TestMethod]
        public void When_SubscribeIsCalled_AndRepsitoryReturnsFalse_Then_UninitializedEntityIsReturned()
        {
            // Setup
            var subscription = new SubscriptionEntity
            {
                Username = "bbc",
                AccessToken = "testtoken",
            };

            _repository.Setup(_ => _.Add(It.IsAny<SubscriptionDataModel>())).Returns(new SubscriptionDataModel
            {
                Username = "bbc",
                AccessToken = "testtoken"
            });

            var result = _service.Subscribe(subscription);

            // Assert
            Assert.AreEqual(0, result.TotalNotificationsPushed);
            Assert.AreEqual(DateTime.MinValue, result.CreationTime);
        }

        [TestMethod]
        public void When_SubscribeIsCalled_AndSubscriptionIsAlreadyAdded_Then_UninitializedEntityIsReturned()
        {
            // Setup
            var subscription = new SubscriptionEntity
            {
                Username = "bbc",
                AccessToken = "testtoken"
            };

            _repository.Setup(_ => _.Add(It.IsAny<SubscriptionDataModel>())).Throws<DuplicateSubscriptionException>();

            var result = _service.Subscribe(subscription);

            // Assert
            Assert.AreEqual(DateTime.MinValue, result.CreationTime);
        }

        [TestMethod]
        public async Task When_SendNotificationsIsCalled_AndSubscriptionAreNotified_Then_SuccessMessageIsReceived()
        {
            // Setup
            var subscription = new SubscriptionDataModel
            {
                Username = "bbc",
                AccessToken = "testtoken"
            };

            _repository.Setup(_ => _.GetSubscription("bbc")).Returns(subscription);
            _repository.Setup(_ => _.Update(It.IsAny<SubscriptionDataModel>())).Returns(true);
            _client.Setup(_ => _.PostAsync("https://api.pushbullet.com/v2/pushes", null, It.IsAny<StringContent>())).ReturnsAsync(new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.Accepted });

            var result = await _service.SendNotification(new NotificationEntity { Username = "bbc", NoteText = "Text", NoteTitle = "Title" });

            // Assert
            Assert.AreEqual(true, result.IsSent);
            Assert.AreEqual("Success", result.StatusText);
        }

        [TestMethod]
        public async Task When_SendNotificationsIsCalled_AndSubscriptionAreNotNotified_Then_FailureMessageIsReceived()
        {
            // Setup
            var subscription = new SubscriptionDataModel
            {
                Username = "bbc",
                AccessToken = "testtoken"
            };

            _repository.Setup(_ => _.GetSubscription("bbc")).Returns(subscription);
            _client.Setup(_ => _.PostAsync("https://api.pushbullet.com/v2/pushes", null, It.IsAny<StringContent>())).ReturnsAsync(new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadGateway });

            var result = await _service.SendNotification(new NotificationEntity { Username = "bbc", NoteText = "Text", NoteTitle = "Title" });

            // Assert
            Assert.AreEqual(false, result.IsSent);
            Assert.AreEqual("Unable to send notification", result.StatusText);
        }
    }
}
