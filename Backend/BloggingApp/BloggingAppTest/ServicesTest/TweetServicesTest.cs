using BloggingApp.Contexts;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Repositories;
using BloggingApp.Repositories.TweetRequest;
using BloggingApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAppTest.ServicesTest
{
    public class TweetServicesTest
    {
        BloggingAppContext context;
        ITweetServices tweetServices;
        int UserId;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                       .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);

            IRepository<int, Comment> commentRepository = new CommentRepository(context);
            IRepository<int, Reply> replyRepository = new ReplyRepository(context);
            IRepository<int, RetweetComment> retweetCommentRepository = new RetweetCommentRepository(context);
            IRepository<int, RetweetCommentReply> retweetCommentReplyRepository = new RetweetCommentReplyRepository(context);
            IRepository<int, Tweet> tweetRepository = new TweetRepository(context);
            IRepository<int, TweetMentions> tweetMentionsRepository = new TweetMentionsRepository(context);
            IRepository<int, TweetHashTags> tweetHashTagsRepository = new TweetHashTagsRepository(context);
            IRepository<int, HashTags> hashTagsRepository = new HashTagRepository(context);
            IRepository<int, User> userRepository = new UserRepository(context);
            IRepository<int, TweetFiles> tweetFilesRepository = new TweetFilesRepository(context);
            IRepository<int, Retweet> retweetRepository = new RetweetRepository(context);
            IRepository<int, TweetLikes> tweetLikesRepository = new TweetLikesRepository(context);
            IRepository<int, RetweetLikes> retweetLikesRepository = new RetweetLikesRepository(context);
            IRepository<int, RetweetMentions> retweetMentionsRepository = new RetweetMentionRepository(context);
            IRepository<int, RetweetHashTags> retweetHashTagsRepository = new RetweetHashTagRepository(context);
            IRepository<int, UserNotification> userNotificationRepository = new UserNotificationRepository(context);
            IRepository<int, Follow> followRepository = new FollowRepository(context);
            TweetRequestForTweetFilesRepository tweetRequestForTweetFiles = new TweetRequestForTweetFilesRepository(context);

            tweetServices = new TweetServices(tweetRepository, tweetMentionsRepository, userRepository,
                tweetHashTagsRepository, hashTagsRepository, tweetFilesRepository,
                tweetRequestForTweetFiles, retweetRepository, tweetLikesRepository,
                retweetLikesRepository, retweetMentionsRepository, retweetHashTagsRepository, userNotificationRepository,
                followRepository, commentRepository, replyRepository, retweetCommentRepository, retweetCommentReplyRepository);


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
        }

        [Test]
        public async Task AddTweetSuccessTest()
        {
            //Arrange

            List<string> MentionsList = new List<string>();
            MentionsList.Add("sanjai25");
            MentionsList.Add("gayathri03");

            List<string> HashTagsList = new List<string>();
            HashTagsList.Add("css");
            HashTagsList.Add("coding");

            AddUserTweetContent addUserTweetContent = new AddUserTweetContent();
            addUserTweetContent.TweetContent = "TweetContent";
            addUserTweetContent.IsCommentEnable = "Yes";
            addUserTweetContent.UserId = UserId;
            addUserTweetContent.TweetMentions = MentionsList;
            addUserTweetContent.TweetHashtags = HashTagsList;

            var result = await tweetServices.AddTweetContentByUser(addUserTweetContent);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddTweetFailTest()
        {
            List<string> MentionsList = new List<string>();
            MentionsList.Add("sanjai25");
            MentionsList.Add("gayathri03");

            List<string> HashTagsList = new List<string>();
            HashTagsList.Add("css");
            HashTagsList.Add("coding");

            AddUserTweetContent addUserTweetContent = new AddUserTweetContent();
            addUserTweetContent.TweetContent = "TweetContent";
            addUserTweetContent.IsCommentEnable = "Yes";
            addUserTweetContent.UserId = 0;
            addUserTweetContent.TweetMentions = MentionsList;
            addUserTweetContent.TweetHashtags = HashTagsList;
            ///Action
            var exception = Assert.ThrowsAsync<Exception>(async () => await tweetServices.AddTweetContentByUser(addUserTweetContent));
            Console.WriteLine(exception.Message);
            //Assert
            Assert.NotNull(exception.Message);
        }

        [Test]
        public async Task AddTweetFilesSuccessTest()
        {
            TweetFiles tweetFiles = new TweetFiles();
            tweetFiles.File1 = "asc";
            tweetFiles.File2 = "asd";
            tweetFiles.TweetId = 0;

            var result = await tweetServices.AddTweetFiles(tweetFiles);

            //Assert
            Assert.IsNotNull(result);

        }
        [Test]
        public async Task AddTweetFilesFailTest()
        {
            TweetFiles tweetFiles = new TweetFiles();
            tweetFiles.File1 = "asc";
            tweetFiles.File2 = "asd";
            tweetFiles.TweetId = -1;
            ///Action
            var exception = Assert.ThrowsAsync<Exception>(async () => await tweetServices.AddTweetFiles(tweetFiles));
            //Assert
            Assert.NotNull(exception.Message);
        }

        [Test]
        public async Task TweetsFeederSuccessTest()
        {
            List<string> MentionsList = new List<string>();
            MentionsList.Add("sanjai25");
            MentionsList.Add("gayathri03");

            List<string> HashTagsList = new List<string>();
            HashTagsList.Add("css");
            HashTagsList.Add("coding");

            AddUserTweetContent addUserTweetContent = new AddUserTweetContent();
            addUserTweetContent.TweetContent = "TweetContent";
            addUserTweetContent.IsCommentEnable = "Yes";
            addUserTweetContent.UserId = UserId;
            addUserTweetContent.TweetMentions = MentionsList;
            addUserTweetContent.TweetHashtags = HashTagsList;

            var result = await tweetServices.AddTweetContentByUser(addUserTweetContent);

            AddRetweetDTO addRetweetDTO = new AddRetweetDTO();
            addRetweetDTO.RetweetContent = "Hi";
            addRetweetDTO.IsCommentEnable = "Yes";
            addRetweetDTO.UserId = 1;
            addRetweetDTO.ActualTweetId = result.TweetId;
            addRetweetDTO.RetweetMentions = MentionsList;
            addRetweetDTO.RetweetHashTags = HashTagsList;
            var result1 = await tweetServices.AddRetweetContent(addRetweetDTO);

            var finalresult = await tweetServices.TweetsFeeder(1);

            //Assert
            Assert.IsNotNull(result);

        }

        [Test]
        public async Task TweetDetailsFeederSuccessTest()
        {
            List<string> MentionsList = new List<string>();
            MentionsList.Add("sanjai25");
            MentionsList.Add("gayathri03");

            List<string> HashTagsList = new List<string>();
            HashTagsList.Add("css");
            HashTagsList.Add("coding");

            AddUserTweetContent addUserTweetContent = new AddUserTweetContent();
            addUserTweetContent.TweetContent = "TweetContent";
            addUserTweetContent.IsCommentEnable = "Yes";
            addUserTweetContent.UserId = UserId;
            addUserTweetContent.TweetMentions = MentionsList;
            addUserTweetContent.TweetHashtags = HashTagsList;

            var result = await tweetServices.AddTweetContentByUser(addUserTweetContent);

            TweetFiles tweetFiles = new TweetFiles();
            tweetFiles.File1 = "asc";
            tweetFiles.File2 = "asd";
            tweetFiles.TweetId = result.TweetId;
            var file = await tweetServices.AddTweetFiles(tweetFiles);

            AddRetweetDTO addRetweetDTO = new AddRetweetDTO();
            addRetweetDTO.RetweetContent = "Hi";
            addRetweetDTO.IsCommentEnable = "Yes";
            addRetweetDTO.UserId = 1;
            addRetweetDTO.ActualTweetId = result.TweetId;
            addRetweetDTO.RetweetMentions = MentionsList;
            addRetweetDTO.RetweetHashTags = HashTagsList;
            var result1 = await tweetServices.AddRetweetContent(addRetweetDTO);

            TweetDetailsDTO tweetDetailsDTO = new TweetDetailsDTO();
            tweetDetailsDTO.TweetId = result.TweetId;
            tweetDetailsDTO.UserId = UserId;
            tweetDetailsDTO.TweetType = "Tweet";
            var finalresult = await tweetServices.TweetDetailsFeeder(tweetDetailsDTO);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task RetweetDetailsFeederSuccessTest()
        {
            List<string> MentionsList = new List<string>();
            MentionsList.Add("sanjai25");
            MentionsList.Add("gayathri03");

            List<string> HashTagsList = new List<string>();
            HashTagsList.Add("css");
            HashTagsList.Add("coding");

            AddUserTweetContent addUserTweetContent = new AddUserTweetContent();
            addUserTweetContent.TweetContent = "TweetContent";
            addUserTweetContent.IsCommentEnable = "Yes";
            addUserTweetContent.UserId = UserId;
            addUserTweetContent.TweetMentions = MentionsList;
            addUserTweetContent.TweetHashtags = HashTagsList;

            var result = await tweetServices.AddTweetContentByUser(addUserTweetContent);

            TweetFiles tweetFiles = new TweetFiles();
            tweetFiles.File1 = "asc";
            tweetFiles.File2 = "asd";
            tweetFiles.TweetId = result.TweetId;
            var file = await tweetServices.AddTweetFiles(tweetFiles);

            AddRetweetDTO addRetweetDTO = new AddRetweetDTO();
            addRetweetDTO.RetweetContent = "Hi";
            addRetweetDTO.IsCommentEnable = "Yes";
            addRetweetDTO.UserId = 1;
            addRetweetDTO.ActualTweetId = result.TweetId;
            addRetweetDTO.RetweetMentions = MentionsList;
            addRetweetDTO.RetweetHashTags = HashTagsList;
            var result1 = await tweetServices.AddRetweetContent(addRetweetDTO);

            RetweetDetailsDTO retweetDetailsDTO = new RetweetDetailsDTO();
            retweetDetailsDTO.RetweetId = 1;
            retweetDetailsDTO.TweetType = "Retweet";
            retweetDetailsDTO.UserId = UserId;
            var finalr = await tweetServices.RetweetDetailsFeeder(retweetDetailsDTO);

            Assert.IsNotNull(finalr);
        }
    }
}
