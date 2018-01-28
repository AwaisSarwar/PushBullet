using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotificationService.Repositories.DataModels;
using NotificationService.Repositories;
using Moq;
using NotificationService.Repositories.Exceptions;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class SubscriptionRepositoryTests
    {
        [TestMethod]
        public void When_AddIsCalled_AndSubscriptionIsNull_Then_SubscriptionIsNotAddedAndNullIsReturned()
        {
            SubscriptionDataModel subscription = null;

            var repository = new SubscriptionRepository();
            var result = repository.Add(subscription);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void When_AddIsCalled_Then_SubscriptionIsAdded()
        {           
            var subscription = new SubscriptionDataModel
            {
                Username = "bbc",
                AccessToken = "testtoken",
                TotalNotificationsPushed = 1,
            };

            var repository = new SubscriptionRepository();
            var result = repository.Add(subscription);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, subscription.TotalNotificationsPushed);
            Assert.AreEqual(DateTime.Now.ToString("dd MM yyyy"), subscription.CreationTime.ToString("dd MM yyyy"));
        }

        [TestMethod]
        public void When_AddIsCalled_AndSubscriptionIsAlreadyAdded_Then_DuplicateSubscriptionExceptionIsThrown()
        {
            var subscription = new SubscriptionDataModel
            {
                Username = "bbc",
                AccessToken = "testtoken",
                TotalNotificationsPushed = 1,
            };

            var repository = new SubscriptionRepository();
            var result = repository.Add(subscription);

            try
            {
                result = repository.Add(subscription);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsTrue(ex is DuplicateSubscriptionException);
            }
        }

        [TestMethod]
        public void When_GetSubscriptionIsCalled_AndSubscriptionIsPresent_Then_SubscriptionIsReturned()
        {
            var repository = new SubscriptionRepository();

            var subscription1 = new SubscriptionDataModel
            {
                Username = "bbc",
                AccessToken = "testtoken",
                TotalNotificationsPushed = 0,
            };            
            repository.Add(subscription1);

            var subscription2 = new SubscriptionDataModel
            {
                Username = "bbc2",
                AccessToken = "newtoken",
                TotalNotificationsPushed = 0,
            };
            repository.Add(subscription2);

            var result = repository.GetSubscription("bbc");

            // Assert
            Assert.AreEqual(subscription1.Username, result.Username);
            Assert.AreEqual(subscription1.AccessToken, result.AccessToken);
        }

        [TestMethod]
        public void When_GetSubscriptionIsCalled_AndSubscriptionIsNotPresent_Then_SubscriptionListIsEmpty()
        {
            var repository = new SubscriptionRepository();

            var subscription1 = new SubscriptionDataModel
            {
                Username = "bbc",
                AccessToken = "testtoken",
                TotalNotificationsPushed = 0,
            };
            repository.Add(subscription1);

            var subscription2 = new SubscriptionDataModel
            {
                Username = "bbc2",
                AccessToken = "newtoken",
                TotalNotificationsPushed = 0,
            };
            repository.Add(subscription2);

            var result = repository.GetSubscription("bbc1");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void When_UpdateIsCalled_AndSubscriptionIsPresent_Then_TrueIsReturned()
        {
            var repository = new SubscriptionRepository();

            var subscription1 = new SubscriptionDataModel
            {
                Username = "bbc",
                AccessToken = "testtoken",
                TotalNotificationsPushed = 0,
            };
            repository.Add(subscription1);

            var subscription2 = new SubscriptionDataModel
            {
                Username = "bbc2",
                AccessToken = "newtoken",
                TotalNotificationsPushed = 0,
            };
            repository.Add(subscription2);

            var result = repository.Update(new SubscriptionDataModel { AccessToken = "newtoken", Username = "bbc" });

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void When_UpdateIsCalled_AndSubscriptionIsNotPresent_Then_FalseIsReturned()
        {
            var repository = new SubscriptionRepository();

            var subscription1 = new SubscriptionDataModel
            {
                Username = "bbc",
                AccessToken = "testtoken",
                TotalNotificationsPushed = 0,
            };
            repository.Add(subscription1);

            var subscription2 = new SubscriptionDataModel
            {
                Username = "bbc2",
                AccessToken = "newtoken",
                TotalNotificationsPushed = 0,
            };
            repository.Add(subscription2);

            var result = repository.Update(new SubscriptionDataModel { AccessToken = "newToken", Username = "bbc1" });

            // Assert
            Assert.IsFalse(result);
        }
    }
}
