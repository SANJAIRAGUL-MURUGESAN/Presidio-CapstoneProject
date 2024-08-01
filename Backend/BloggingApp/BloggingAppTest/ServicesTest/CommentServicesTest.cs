using BloggingApp.Contexts;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Repositories.CommentRequest;
using BloggingApp.Repositories.RetweetCommentRequest;
using BloggingApp.Repositories;
using BloggingApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Models.CommentDTOs;
using BloggingApp.Models.ReplyDTOs;

namespace BloggingAppTest.ServicesTest
{
    public class CommentServicesTest
    {
        BloggingAppContext context;
        ICommentServices commentServices;
        int UserId;
        int TweetId;

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
            IRepository<int, User> userRepository = new UserRepository(context);
            IRepository<int, TweetCommentLikes> tweetCommentLikesRepository = new TweetCommentLikeRepository(context);
            IRepository<int, TweetReplyLikes> tweetReplyLikesRepository = new TweetCommentReplyLikeRepository(context);
            IRepository<int, RetweetCommentLikes> retweetCommentLikesRepository = new RetweetCommentLikesRepository(context);
            IRepository<int, RetweetCommentReplyLikes> retweetCommentReplyLikesRepository = new RetweetCommentReplyLikesRepository(context);
            IRepository<int, UserNotification> userNotificationRepository = new UserNotificationRepository(context);
            IRepository<int, Retweet> retweetRepository = new RetweetRepository(context);
            CommentRequestForRepliesRepository commentRequestForRepliesRepository = new CommentRequestForRepliesRepository(context);
            RetweetCommentRequestforRepliesRepository retweetCommentRequestForRepliesRepository = new RetweetCommentRequestforRepliesRepository(context);

            commentServices = new CommentServices(
                commentRepository,
                replyRepository,
                userRepository,
                tweetRepository,
                retweetRepository,
                commentRequestForRepliesRepository,
                retweetCommentRepository,
                retweetCommentReplyRepository,
                retweetCommentRequestForRepliesRepository,
                tweetCommentLikesRepository,
                tweetReplyLikesRepository,
                retweetCommentLikesRepository,
                retweetCommentReplyLikesRepository,
                userNotificationRepository);

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
        public async Task AddTweetCommentSuccessTest()
        {

            AddCommentDTO comment = new AddCommentDTO();
            comment.CommentContent = "Comment";
            comment.UserId = UserId;
            comment.TweetId = 1;
            var result = await commentServices.AddComment(comment);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddTweetCommentFailTest()
        {

            AddCommentDTO comment = new AddCommentDTO();
            comment.CommentContent = "Comment";
            comment.UserId = UserId;
            comment.TweetId = TweetId;
            var exception = Assert.ThrowsAsync<Exception>(async () => await commentServices.AddComment(comment));
            //Assert
            Assert.AreEqual("Expected exception message", exception.Message);

        }


        [Test]
        public async Task AddCommentReplySuccessTest()
        {
            AddCommentDTO comment = new AddCommentDTO();
            comment.CommentContent = "Comment";
            comment.UserId = UserId;
            comment.TweetId = 1;
            var result1 = await commentServices.AddComment(comment);
            AddReplyDTO addReplyDTO = new AddReplyDTO();
            addReplyDTO.ReplyContent = "reply";
            addReplyDTO.UserId = 1;
            addReplyDTO.Comment_ReplyId = 1;
            var result = await commentServices.AddCommentReply(addReplyDTO);

            Assert.IsNotNull(result);
        }


        [Test]
        public async Task AddCommentReplyFailTest()
        {
            AddReplyDTO addReplyDTO = new AddReplyDTO();
            addReplyDTO.ReplyContent = "reply";
            addReplyDTO.UserId = 1;
            addReplyDTO.Comment_ReplyId = 1;
            var exception = Assert.ThrowsAsync<Exception>(async () => await commentServices.AddCommentReply(addReplyDTO));
            //Assert
            Assert.AreEqual("Expected exception message", exception.Message);
        }

        [Test]
        public async Task ReturnCommentsSuccessTest()
        {
            AddCommentDTO comment = new AddCommentDTO();
            comment.CommentContent = "Comment";
            comment.UserId = UserId;
            comment.TweetId = 1;
            var result1 = await commentServices.AddComment(comment);
            AddReplyDTO addReplyDTO = new AddReplyDTO();
            addReplyDTO.ReplyContent = "reply";
            addReplyDTO.UserId = 1;
            addReplyDTO.Comment_ReplyId = 1;
            var result = await commentServices.AddCommentReply(addReplyDTO);

            TweetCommentDetails tweetCommentDetails = new TweetCommentDetails();
            tweetCommentDetails.TweetId = 1;
            tweetCommentDetails.UserId = UserId;

            var added = await commentServices.ReturnComments(tweetCommentDetails);

            Assert.IsNotNull(added);
        }

        [Test]
        public async Task AddReplyTOReplySuccessTest()
        {
            AddCommentDTO comment = new AddCommentDTO();
            comment.CommentContent = "Comment";
            comment.UserId = UserId;
            comment.TweetId = 1;
            var result1 = await commentServices.AddComment(comment);
            AddReplytoRelpyDTO addReplytoRelpyDTO = new AddReplytoRelpyDTO();
            addReplytoRelpyDTO.ReplyContent = "reply";
            addReplytoRelpyDTO.UserId = UserId;
            addReplytoRelpyDTO.CommentId = 1;
            addReplytoRelpyDTO.ReplyId = 1;
            var added = await commentServices.AddReplyTOReply(addReplytoRelpyDTO);

            Assert.IsNotNull(added);
        }

        [Test]
        public async Task AddReplyTOReplyFailTest()
        {
            AddCommentDTO comment = new AddCommentDTO();
            comment.CommentContent = "Comment";
            comment.UserId = UserId;
            comment.TweetId = 1;
            var result1 = await commentServices.AddComment(comment);
            AddReplytoRelpyDTO addReplytoRelpyDTO = new AddReplytoRelpyDTO();
            addReplytoRelpyDTO.ReplyContent = "reply";
            addReplytoRelpyDTO.UserId = UserId;
            addReplytoRelpyDTO.CommentId = 100;
            addReplytoRelpyDTO.ReplyId = 1;
            var added = await commentServices.AddReplyTOReply(addReplytoRelpyDTO);
            var exception = Assert.ThrowsAsync<Exception>(async () => commentServices.AddReplyTOReply(addReplytoRelpyDTO));
            //Assert
            Assert.IsNull(exception.Message);
        }

        [Test]
        public async Task AddCommenttoRetweetSuccessTest()
        {
            AddRetweetCommentDTO comment = new AddRetweetCommentDTO();
            comment.CommentContent = "Comment";
            comment.UserId = UserId;
            comment.RetweetId = 1;
            var result1 = await commentServices.AddCommenttoRetweet(comment);
            Assert.IsNotNull(result1);
        }

        [Test]
        public async Task AddRetweetCommentReplySuccessTest()
        {
            AddRetweetCommentReplyDTO reply = new AddRetweetCommentReplyDTO();
            reply.ReplyContent = "reply";
            reply.UserId = 1;
            reply.Comment_ReplyId = 1;
            var result1 = await commentServices.AddRetweetCommentReply(reply);
            Assert.IsNotNull(result1);
        }

        [Test]
        public async Task AddRetweetCommentReplyTOReplySuccessTest()
        {
            AddRetweetCommentReplytoRelpy reply = new AddRetweetCommentReplytoRelpy();
            reply.ReplyContent = "reply";
            reply.UserId = 1;
            reply.RetweetCommentId = 1;
            reply.ReweetCommentReplyId = 1;
            var result1 = await commentServices.AddRetweetCommentReplyTOReply(reply);
            Assert.IsNotNull(result1);
        }


        [Test]
        public async Task ReturnRetweetCommentsSuccessTest()
        {
            RetweetCommentDetails reply = new RetweetCommentDetails();
            reply.RetweetId = 1;
            reply.UserId = 1;
            var result1 = await commentServices.ReturnRetweetComments(reply);
            Assert.IsNotNull(result1);
        }


    }
}
