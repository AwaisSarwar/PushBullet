using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotificationService.Extensions;
using NotificationService.Controllers.Requests;
using NotificationService.Repositories.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void When_ToSubsciptionEntityIsCalledOnSubscribeRequest_Then_SubscriptionEntityIsReturned()
        {
            var subscribeRequest = new SubscribeRequest
            {
                AccessToken = "Token",
                Username = "bbc"
            };

            var entity = subscribeRequest.ToSubscriptionEntity();

            // Assert
            Assert.AreEqual(subscribeRequest.Username, entity.Username);
            Assert.AreEqual(subscribeRequest.AccessToken, entity.AccessToken);
        }

        [TestMethod]
        public void When_ToSubsciptionEntityIsCalledOnSubscriptionDataModel_Then_SubscriptionEntityIsReturned()
        {
            var subscriptionDataModel = new SubscriptionDataModel
            {
                AccessToken = "Token",
                Username = "bbc",
                CreationTime = DateTime.Now,
                TotalNotificationsPushed = 1
            };

            var entity = subscriptionDataModel.ToSubscriptionEntity();

            // Assert
            Assert.AreEqual(subscriptionDataModel.Username, entity.Username);
            Assert.AreEqual(subscriptionDataModel.AccessToken, entity.AccessToken);
            Assert.AreEqual(subscriptionDataModel.CreationTime, entity.CreationTime);
            Assert.AreEqual(subscriptionDataModel.TotalNotificationsPushed, entity.TotalNotificationsPushed);
        }

        [TestMethod]
        public void When_ToSubsciptionEntitiesIsCalledOnSubscriptionDataModelList_Then_SubscriptionEntityListIsReturned()
        {
            var subscriptionDataModel1 = new SubscriptionDataModel
            {
                AccessToken = "Token",
                Username = "bbc1",
                CreationTime = DateTime.Now,
                TotalNotificationsPushed = 1
            };

            var subscriptionDataModel2 = new SubscriptionDataModel
            {
                AccessToken = "Token",
                Username = "bbc2",
                CreationTime = DateTime.Now,
                TotalNotificationsPushed = 1
            };

            var subscriptionDataModels = new List<SubscriptionDataModel>();
            subscriptionDataModels.Add(subscriptionDataModel1);
            subscriptionDataModels.Add(subscriptionDataModel2);

            var entities = subscriptionDataModels.ToSubscriptionEntities();

            // Assert
            Assert.AreEqual(subscriptionDataModels.Count, entities.Count);
            Assert.AreEqual(subscriptionDataModels.First().Username, entities.First().Username);
            Assert.AreEqual(subscriptionDataModels.Last().Username, entities.Last().Username);
        }
    }
}
