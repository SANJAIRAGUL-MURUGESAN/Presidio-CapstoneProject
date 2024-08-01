using BloggingApp.Contexts;
using BloggingApp.Exceptions.RetweetCommentReplyRepository;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Models;
using BloggingApp.Models.ReplyDTOs;
using BloggingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAppTest.RepositoryTests
{
    public class RetweetCommentReplyRepositoryTest
    {
        BloggingAppContext context;
        RetweetCommentReplyRepository RetweetCommentReplyRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            RetweetCommentReplyRepository = new RetweetCommentReplyRepository(context);
        }

        [Test]
        public async Task AddRetweetCommentReplySuccessTest()
        {
            //Arrange

            RetweetCommentReply retweetCommentReply = new RetweetCommentReply();
            retweetCommentReply.ReplyContent = "CommentReply";
            retweetCommentReply.ReplyDateTime = DateTime.Now;
            retweetCommentReply.ReplyType = "Comment";
            retweetCommentReply.UserId = 1;
            retweetCommentReply.RetweetCommentId = 1;
            var result = await RetweetCommentReplyRepository.Add(retweetCommentReply);

            //Assert
            Assert.AreEqual(1, result.UserId);
        }

        [Test]
        public async Task DeleteRetweetCommentReplySuccessTest()
        {
            //Arrange
            RetweetCommentReply retweetCommentReply = new RetweetCommentReply();
            retweetCommentReply.ReplyContent = "CommentReply";
            retweetCommentReply.ReplyDateTime = DateTime.Now;
            retweetCommentReply.ReplyType = "Comment";
            retweetCommentReply.UserId = 1;
            retweetCommentReply.RetweetCommentId = 1;
            var result = await RetweetCommentReplyRepository.Add(retweetCommentReply);

            var DeletedTweet = await RetweetCommentReplyRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.UserId);
        }


        [Test]
        public async Task RetweetCommentReplyDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetCommentReplyFoundException>(async () => await RetweetCommentReplyRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No such Retweet Comment Reply Found!", exception.Message);
        }

        [Test]
        public async Task GetRetweetCommentReplyByKeySuccessTest()
        {
            //Arrange
            RetweetCommentReply retweetCommentReply = new RetweetCommentReply();
            retweetCommentReply.ReplyContent = "CommentReply";
            retweetCommentReply.ReplyDateTime = DateTime.Now;
            retweetCommentReply.ReplyType = "Comment";
            retweetCommentReply.UserId = 1;
            retweetCommentReply.RetweetCommentId = 1;
            var result = await RetweetCommentReplyRepository.Add(retweetCommentReply);

            var AddedTweet = await RetweetCommentReplyRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.UserId);
        }

        [Test]
        public async Task GetRetweetCommentReplybyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetCommentReplyFoundException>(async () => await RetweetCommentReplyRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No such Retweet Comment Reply Found!", exception.Message);
        }

        [Test]
        public async Task GetAllRetweetCommentReplySuccessTest()
        {
            //Arrange
            var result = await RetweetCommentReplyRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllRetweetCommentReplyFailTest()
        {
            //Arrange
            var result = await RetweetCommentReplyRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task UpdateRetweetCommentReplySuccessTest()
        {
            //Arrange
            RetweetCommentReply retweetCommentReply = new RetweetCommentReply();
            retweetCommentReply.ReplyContent = "CommentReply";
            retweetCommentReply.ReplyDateTime = DateTime.Now;
            retweetCommentReply.ReplyType = "Comment";
            retweetCommentReply.UserId = 1;
            retweetCommentReply.RetweetCommentId = 1;
            var result = await RetweetCommentReplyRepository.Add(retweetCommentReply);

            var AddedTweet = await RetweetCommentReplyRepository.GetbyKey(result.Id);
            AddedTweet.UserId = 123;

            var UpdatedResult = await RetweetCommentReplyRepository.Update(result);

            //Assert
            Assert.AreEqual(123, result.UserId);
        }


        [Test]
        public async Task UpdateRetweetCommentReplyFailTest()
        {
            //Arrange
            RetweetCommentReply retweetCommentReply = new RetweetCommentReply();
            retweetCommentReply.ReplyContent = "CommentReply";
            retweetCommentReply.ReplyDateTime = DateTime.Now;
            retweetCommentReply.ReplyType = "Comment";
            retweetCommentReply.UserId = 1;
            retweetCommentReply.RetweetCommentId = 1;
            var result = await RetweetCommentReplyRepository.Add(retweetCommentReply);

            var AddedTweet = await RetweetCommentReplyRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetCommentReplyFoundException>(async () => await RetweetCommentReplyRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No such Retweet Comment Reply Found!", exception.Message);
        }

    }
}
