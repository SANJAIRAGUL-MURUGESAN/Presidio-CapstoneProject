using BloggingApp.Contexts;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Exceptions.UserNotifications;
using BloggingApp.Models;
using BloggingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAppTest.RepositoryTests
{
    public class UserNotificationRepositoryTest
    {

        BloggingAppContext context;
        UserNotificationRepository userNotificationRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            userNotificationRepository = new UserNotificationRepository(context);
        }

        [Test]
        public async Task AddUserNotificationSuccessTest()
        {
            //Arrange

            UserNotification userNotificationm = new UserNotification();
            userNotificationm.UserId = 1;
            userNotificationm.NotificationPost = "asssdsfs";
            userNotificationm.IsUserSeen = "No";
            userNotificationm.ContentDateTime = DateTime.Now;
            userNotificationm.TweetType = "Tweet";
            userNotificationm.TweetId = 1;
            userNotificationm.NotificatioContent = " Mentioned you in a Post";
            var result = await userNotificationRepository.Add(userNotificationm);

            //Assert
            Assert.AreEqual(1, result.UserId);
        }


        [Test]
        public async Task DeleteUserNotificationSuccessTest()
        {
            //Arrange
            UserNotification userNotificationm = new UserNotification();
            userNotificationm.UserId = 1;
            userNotificationm.NotificationPost = "asssdsfs";
            userNotificationm.IsUserSeen = "No";
            userNotificationm.ContentDateTime = DateTime.Now;
            userNotificationm.TweetType = "Tweet";
            userNotificationm.TweetId = 1;
            userNotificationm.NotificatioContent = " Mentioned you in a Post";
            var result = await userNotificationRepository.Add(userNotificationm);

            var DeletedTweet = await userNotificationRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.UserId);
        }

        [Test]
        public async Task TweetUserNotificationDailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchUserNotificationFoundException>(async () => await userNotificationRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such User Notification Found!", exception.Message);
        }

        [Test]
        public async Task GetUserNotificationByKeySuccessTest()
        {
            //Arrange
            UserNotification userNotificationm = new UserNotification();
            userNotificationm.UserId = 1;
            userNotificationm.NotificationPost = "asssdsfs";
            userNotificationm.IsUserSeen = "No";
            userNotificationm.ContentDateTime = DateTime.Now;
            userNotificationm.TweetType = "Tweet";
            userNotificationm.TweetId = 1;
            userNotificationm.NotificatioContent = " Mentioned you in a Post";
            var result = await userNotificationRepository.Add(userNotificationm);

            var AddedTweet = await userNotificationRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.UserId);
        }


        [Test]
        public async Task GetUserNotificationbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchUserNotificationFoundException>(async () => await userNotificationRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such User Notification Found!", exception.Message);
        }


        [Test]
        public async Task GetAllUserNotificationsSuccessTest()
        {
            //Arrange
            var result = await userNotificationRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllUserNotificationsFailTest()
        {
            //Arrange
            var result = await userNotificationRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateUserNotificationSuccessTest()
        {
            //Arrange
            UserNotification userNotificationm = new UserNotification();
            userNotificationm.UserId = 1;
            userNotificationm.NotificationPost = "asssdsfs";
            userNotificationm.IsUserSeen = "Yes";
            userNotificationm.ContentDateTime = DateTime.Now;
            userNotificationm.TweetType = "Tweet";
            userNotificationm.TweetId = 1;
            userNotificationm.NotificatioContent = " Mentioned you in a Post";
            var result = await userNotificationRepository.Add(userNotificationm);

            var AddedTweet = await userNotificationRepository.GetbyKey(result.Id);
            AddedTweet.IsUserSeen = "No";

            var UpdatedResult = await userNotificationRepository.Update(result);

            //Assert
            Assert.AreEqual("No", result.IsUserSeen);
        }

        [Test]
        public async Task UpdateUserNotificationFailTest()
        {
            //Arrange
            UserNotification userNotificationm = new UserNotification();
            userNotificationm.UserId = 1;
            userNotificationm.NotificationPost = "asssdsfs";
            userNotificationm.IsUserSeen = "Yes";
            userNotificationm.ContentDateTime = DateTime.Now;
            userNotificationm.TweetType = "Tweet";
            userNotificationm.TweetId = 1;
            userNotificationm.NotificatioContent = " Mentioned you in a Post";
            var result = await userNotificationRepository.Add(userNotificationm);

            var AddedTweet = await userNotificationRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchUserNotificationFoundException>(async () => await userNotificationRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such User Notification Found!", exception.Message);
        }

    }
}
