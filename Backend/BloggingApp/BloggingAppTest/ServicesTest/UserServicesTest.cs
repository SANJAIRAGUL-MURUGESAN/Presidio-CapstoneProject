using BloggingApp.Contexts;
using BloggingApp.Exceptions.UserExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Models.CommentDTOs;
using BloggingApp.Models.FollowDTOs;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Repositories;
using BloggingApp.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAppTest.ServicesTest
{
    public class UserServicesTest
    {

        BloggingAppContext context;
        IUserServices userServices;
        int UserId;
        int TweetId;
        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                       .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            IRepository<int, User> userRepository = new UserRepository(context);
            IRepository<int, Follow> followRepository = new FollowRepository(context);
            IRepository<int, UserNotification> userNotificationRepository = new UserNotificationRepository(context);

            // Mock TokenServices
            var tokenServiceMock = new Mock<ITokenServices>();
            tokenServiceMock.Setup(ts => ts.GenerateToken(It.IsAny<User>())).Returns("mockedToken");

            // Create UserServices instance
            userServices = new UserServices(
                context,
                userRepository,
                tokenServiceMock.Object,
                followRepository,
                userNotificationRepository
            );

            User user = new User();
            user.UserName = "Sanjai Ragul M";
            user.UserId = "sanjai25";
            user.UserEmail = "s@gmail.com";
            user.UserPassword = "asd";
            user.UserGender = "string";
            user.UserEmail = "string";
            user.UserMobile = "string";
            user.Location = "string";
            user.IsPremiumHolder = "string";
            user.DateOfBirth = "string";
            user.JoinedDate = DateTime.Now;
            user.BioDescription = "string";
            user.UserProfileImgLink = "string";
            var AddedUser = userRepository.Add(user);
            UserId = AddedUser.Id;
        }

        [Test]
        public async Task RegisterUserSuccessTest()
        {

            RegisterUserDTO user = new RegisterUserDTO();
            user.UserName = "sanjairagulm";
            user.UserId = "sanjai25";
            user.UserEmail = "sanjai@gmail.com";
            user.UserPassword = "asd";
            user.UserGender = "string";
            user.UserEmail = "string";
            user.UserMobile = "string";
            user.Location = "string";
            user.IsPremiumHolder = "string";
            user.DateOfBirth = "string";
            user.Age = 22;
            user.BioDescription = "string";
            user.UserProfileImgLink = "string";
            var result = await userServices.RegisterUser(user);

            Assert.IsNotNull(result);
        }


        [Test]
        public async Task UserLoginSuccessTest()
        {
            UserLoginDTO login = new UserLoginDTO();
            login.Email = "s@gmail.com";
            login.Password = "asd";
            var result = await userServices.UserLogin(login);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UserLoginFailTest()
        {
            UserLoginDTO login = new UserLoginDTO();
            login.Email = "s@gmail.com";
            login.Password = "asd";

            var exception = Assert.ThrowsAsync<InvalidCredentialsException>(async () => await userServices.UserLogin(login));
            //Assert
            Assert.AreEqual("Invalid Username or Password!", exception.Message);

        }
        [Test]
        public async Task ShowFollowersSuccessTest()
        {
            int userid = 1;
            var result = await userServices.ShowFollowers(userid);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task RemoveFollowerSuccessTest()
        {
            RemoveFollowerDTO follower = new RemoveFollowerDTO();
            follower.UserId = 1;
            follower.FollowerId = 1;
            var result = await userServices.RemoveFollower(follower);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddFollowerSuccessTest()
        {
            AddFollowerDTO follower = new AddFollowerDTO();
            follower.UserId = 1;
            follower.FollowerId = 1;
            var result = await userServices.AddFollower(follower);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task ReturnSideBarUserInfoSuccessTest()
        {

            var result = await userServices.ReturnSideBarUserInfo(1);

            Assert.IsNotNull(result);
        }


        [Test]
        public async Task NotificationSenderSuccessTest()
        {
            NotificationUserDetailsDTO notificationUserDetailsDTO = new NotificationUserDetailsDTO();
            notificationUserDetailsDTO.UserId = 1;
            var result = await userServices.NotificationSender(notificationUserDetailsDTO);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateNotificationSuccessTest()
        {

            var result = await userServices.UpdateNotification(1);

            Assert.IsNotNull(result);
        }

    }
}
