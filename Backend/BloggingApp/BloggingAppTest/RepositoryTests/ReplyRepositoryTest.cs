using BloggingApp.Contexts;
using BloggingApp.Exceptions.ReplyExceptions;
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
    public class ReplyRepositoryTest
    {
        BloggingAppContext context;
        ReplyRepository replyRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            replyRepository = new ReplyRepository(context);
        }


        [Test]
        public async Task AddReplySuccessTest()
        {
            //Arrange

            Reply reply = new Reply();
            reply.ReplyContent = "Reply";
            reply.ReplyDateTime = DateTime.Now;
            reply.ReplyType = "Reply";
            reply.UserId = 1;
            reply.CommentId = 1;
            reply.ReplyId = 1;
            var result = await replyRepository.Add(reply);

            //Assert
            Assert.AreEqual(1, result.UserId);
        }

        [Test]
        public async Task DeleteReplySuccessTest()
        {
            //Arrange

            Reply reply = new Reply();
            reply.ReplyContent = "Reply";
            reply.ReplyDateTime = DateTime.Now;
            reply.ReplyType = "Reply";
            reply.UserId = 1;
            reply.CommentId = 1;
            reply.ReplyId = 1;
            var result = await replyRepository.Add(reply);

            var DeletedTweet = await replyRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.UserId);
        }


        [Test]
        public async Task ReplyDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchReplyFoundException>(async () => await replyRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such Reply Found!", exception.Message);
        }

        [Test]
        public async Task GetReplyByKeySuccessTest()
        {
            //Arrange
            Reply reply = new Reply();
            reply.ReplyContent = "Reply";
            reply.ReplyDateTime = DateTime.Now;
            reply.ReplyType = "Reply";
            reply.UserId = 1;
            reply.CommentId = 1;
            reply.ReplyId = 1;
            var result = await replyRepository.Add(reply);


            var AddedTweet = await replyRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.UserId);
        }

        [Test]
        public async Task GetReplybyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchReplyFoundException>(async () => await replyRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such Reply Found!", exception.Message);
        }

        [Test]
        public async Task GetAllReplySuccessTest()
        {
            //Arrange
            var result = await replyRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllReplyFailTest()
        {
            //Arrange
            var result = await replyRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateReplySuccessTest()
        {
            //Arrange
            Reply reply = new Reply();
            reply.ReplyContent = "Reply";
            reply.ReplyDateTime = DateTime.Now;
            reply.ReplyType = "Reply";
            reply.UserId = 1;
            reply.CommentId = 1;
            reply.ReplyId = 1;
            var result = await replyRepository.Add(reply);


            var AddedTweet = await replyRepository.GetbyKey(result.Id);
            AddedTweet.CommentId = 2;

            var UpdatedResult = await replyRepository.Update(result);

            //Assert
            Assert.AreEqual(2, result.CommentId);
        }


        [Test]
        public async Task UpdateReplyFailTest()
        {
            //Arrange
            Reply reply = new Reply();
            reply.ReplyContent = "Reply";
            reply.ReplyDateTime = DateTime.Now;
            reply.ReplyType = "Reply";
            reply.UserId = 1;
            reply.CommentId = 1;
            reply.ReplyId = 1;
            var result = await replyRepository.Add(reply);

            var AddedTweet = await replyRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchReplyFoundException>(async () => await replyRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such Reply Found!", exception.Message);
        }

    }
}
