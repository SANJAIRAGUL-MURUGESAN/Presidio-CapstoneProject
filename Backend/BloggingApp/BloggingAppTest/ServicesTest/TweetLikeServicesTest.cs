using BloggingApp.Contexts;
using BloggingApp.Exceptions.TweetLikesRepository;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Models.CRLikesDTOs;
using BloggingApp.Models.NewFolder;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Repositories;
using BloggingApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAppTest.ServicesTest
{
    public class TweetLikeServicesTest
    {
        BloggingAppContext context;
        ITweetLikesServices tweetLikesServices;
        int UserId;
        int TweetId;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                       .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            // Initialize repositories
            IRepository<int, TweetLikes> tweetLikesRepository = new TweetLikesRepository(context);
            IRepository<int, RetweetLikes> retweetLikesRepository = new RetweetLikesRepository(context);
            IRepository<int, TweetCommentLikes> tweetCommentLikesRepository = new TweetCommentLikeRepository(context);
            IRepository<int, TweetReplyLikes> tweetReplyLikesRepository = new TweetCommentReplyLikeRepository(context);
            IRepository<int, RetweetCommentLikes> retweetCommentLikesRepository = new RetweetCommentLikesRepository(context);
            IRepository<int, RetweetCommentReplyLikes> retweetCommentReplyLikesRepository = new RetweetCommentReplyLikesRepository(context);
            IRepository<int, User> userRepository = new UserRepository(context);
            IRepository<int, UserNotification> userNotificationRepository = new UserNotificationRepository(context);
            IRepository<int, Tweet> tweetRepository = new TweetRepository(context);
            IRepository<int, Retweet> retweetRepository = new RetweetRepository(context);

            // Create TweetLikesServices instance
            tweetLikesServices = new TweetLikesServices(
                tweetLikesRepository,
                retweetLikesRepository,
                tweetCommentLikesRepository,
                tweetReplyLikesRepository,
                retweetCommentLikesRepository,
                retweetCommentReplyLikesRepository,
                userRepository,
                userNotificationRepository,
                tweetRepository,
                retweetRepository
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
            user.Age = 22;
            user.BioDescription = "string";
            user.UserProfileImgLink = "string";
            var AddedUser = userRepository.Add(user);
            UserId = AddedUser.Id;

            List<string> MentionsList = new List<string>();
            MentionsList.Add("sanjai25");
            MentionsList.Add("gayathri03");

            List<string> HashTagsList = new List<string>();
            HashTagsList.Add("css");
            HashTagsList.Add("coding");

            Tweet tweet = new Tweet();
            tweet.TweetContent = "TweetContent";
            tweet.IsCommentEnable = "Yes";
            tweet.UserId = UserId;
            tweet.TweetDateTime = DateTime.Now;
            var result = tweetRepository.Add(tweet);
            TweetId = result.Id;

            Retweet retweet = new Retweet();
            retweet.RetweetContent = "retwet";
            retweet.IsCommentEnable = "Yes";
            retweet.ActualTweetId = TweetId;
            retweet.RetweetDateTime = DateTime.Now;
            var retweetr = retweetRepository.Add(retweet);
        }

        [Test]
        public async Task AddTweetLikesSuccessTest()
        {
            //Arrange
            AddTweetLikesDTO addTweetLikesDTO = new AddTweetLikesDTO();
            addTweetLikesDTO.LikedUserId = 1;
            addTweetLikesDTO.TweetId = 1;
            var result = await tweetLikesServices.AddTweetLikes(addTweetLikesDTO);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddTweetLikesFailTest()
        {
            //Arrange
            AddTweetLikesDTO addTweetLikesDTO = new AddTweetLikesDTO();
            addTweetLikesDTO.LikedUserId = 1;
            addTweetLikesDTO.TweetId = 1000;
            var result = await tweetLikesServices.AddTweetLikes(addTweetLikesDTO);

            var exception = Assert.ThrowsAsync<Exception>(async () => await tweetLikesServices.AddTweetLikes(addTweetLikesDTO));
            Console.WriteLine(exception.Message);
            //Assert
            Assert.AreEqual("Expected exception message", exception.Message);
        }


        [Test]
        public async Task AddRetweetLikesSuccessTest()
        {
            //Arrange
            AddRetweekLikeDTO addTweetLikesDTO = new AddRetweekLikeDTO();
            addTweetLikesDTO.LikedUserId = 1;
            addTweetLikesDTO.RetweetId = 1;
            var result = await tweetLikesServices.AddRetweetLikes(addTweetLikesDTO);

            //Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task AddTweetDisLikesSuccessTest()
        {
            //Arrange
            AddTweetDislikeDTO addTweetLikesDTO = new AddTweetDislikeDTO();
            addTweetLikesDTO.LikedUserId = 1;
            addTweetLikesDTO.TweetId = 1;
            var result = await tweetLikesServices.AddTweetDisLikes(addTweetLikesDTO);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddRetweetDisLikesSuccessTest()
        {
            //Arrange
            AddRetweetDislikeDTO addTweetLikesDTO = new AddRetweetDislikeDTO();
            addTweetLikesDTO.LikedUserId = 1;
            addTweetLikesDTO.RetweetId = 1;
            var result = await tweetLikesServices.AddRetweetDisLikes(addTweetLikesDTO);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddTweetCommentLikesSuccessTest()
        {
            //Arrange
            AddTweetCommentLikesDTO addTweetLikesDTO = new AddTweetCommentLikesDTO();
            addTweetLikesDTO.LikedUserid = 1;
            addTweetLikesDTO.CommentId = 1;
            var result = await tweetLikesServices.AddTweetCommentLikes(addTweetLikesDTO);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddTweetCommentDislikeSuccessTest()
        {
            //Arrange
            AddTweetCommentDislikeDTO addTweetLikesDTO = new AddTweetCommentDislikeDTO();
            addTweetLikesDTO.LikedUserid = 1;
            addTweetLikesDTO.CommentId = 1;
            var result = await tweetLikesServices.AddTweetCommentDislike(addTweetLikesDTO);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddTweetCommentReplyLikesSuccessTest()
        {
            //Arrange
            AddTweetCommentReplyLikeDTO addTweetLikesDTO = new AddTweetCommentReplyLikeDTO();
            addTweetLikesDTO.LikedUserId = 1;
            addTweetLikesDTO.ReplyId = 1;
            var result = await tweetLikesServices.AddTweetCommentReplyLikes(addTweetLikesDTO);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddTweetCommentReplyDislikeSuccessTest()
        {
            //Arrange
            AddTweetCommentReplyDislikeDTO addTweetLikesDTO = new AddTweetCommentReplyDislikeDTO();
            addTweetLikesDTO.LikedUserId = 1;
            addTweetLikesDTO.ReplyId = 1;
            var result = await tweetLikesServices.AddTweetCommentReplyDislike(addTweetLikesDTO);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddRetweetCommentLikesSuccessTest()
        {
            //Arrange
            AddRetweetCommentLikeDTO addTweetLikesDTO = new AddRetweetCommentLikeDTO();
            addTweetLikesDTO.LikedUserid = 1;
            addTweetLikesDTO.RetweetCommentId = 1;
            var result = await tweetLikesServices.AddRetweetCommentLikes(addTweetLikesDTO);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddRetweetCommentDislikeSuccessTest()
        {
            //Arrange
            AddRetweetCommentDislikeDTO addTweetLikesDTO = new AddRetweetCommentDislikeDTO();
            addTweetLikesDTO.LikedUserid = 1;
            addTweetLikesDTO.RetweetCommentId = 1;
            var result = await tweetLikesServices.AddRetweetCommentDislike(addTweetLikesDTO);

            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task AddRetweetCommentReplyLikesSuccessTest()
        {
            //Arrange
            AddRetweetCommentReplyLikeDTO addTweetLikesDTO = new AddRetweetCommentReplyLikeDTO();
            addTweetLikesDTO.LikedUserId = 1;
            addTweetLikesDTO.ReplyCommentReplyId = 1;
            var result = await tweetLikesServices.AddRetweetCommentReplyLikes(addTweetLikesDTO);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddRetweetCommentReplyDislikeSuccessTest()
        {
            //Arrange
            AddRetweetCommentReplyDislikeDTO addTweetLikesDTO = new AddRetweetCommentReplyDislikeDTO();
            addTweetLikesDTO.LikedUserId = 1;
            addTweetLikesDTO.ReplyCommentReplyId = 1;
            var result = await tweetLikesServices.AddRetweetCommentReplyDislike(addTweetLikesDTO);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
