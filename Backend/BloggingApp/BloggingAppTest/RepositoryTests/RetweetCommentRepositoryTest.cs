using BloggingApp.Contexts;
using BloggingApp.Exceptions.RetweetCommentExceptions;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Models;
using BloggingApp.Models.CommentDTOs;
using BloggingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAppTest.RepositoryTests
{
    public class RetweetCommentRepositoryTest
    {
        BloggingAppContext context;
        RetweetCommentRepository retweetCommentRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            retweetCommentRepository = new RetweetCommentRepository(context);
        }

        [Test]
        public async Task AddRetweetCommentSuccessTest()
        {
            //Arrange
            RetweetComment retweetComment = new RetweetComment();
            retweetComment.CommentContent = "Comemnt";
            retweetComment.CommentDateTime = DateTime.Now;
            retweetComment.UserId = 1;
            retweetComment.RetweetId =1 ;
            var result = await retweetCommentRepository.Add(retweetComment);

            //Assert
            Assert.AreEqual(1, result.UserId);
        }

        [Test]
        public async Task DeleteRetweetCommentSuccessTest()
        {
            //Arrange
            RetweetComment retweetComment = new RetweetComment();
            retweetComment.CommentContent = "Comemnt";
            retweetComment.CommentDateTime = DateTime.Now;
            retweetComment.UserId = 1;
            retweetComment.RetweetId = 1;
            var result = await retweetCommentRepository.Add(retweetComment);

            var DeletedTweet = await retweetCommentRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.RetweetId);
        }

        [Test]
        public async Task RetweetCommentDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetCommentFoundException>(async () => await retweetCommentRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such Retweet Comment Found!", exception.Message);
        }

        [Test]
        public async Task GetRetweetCommentByKeySuccessTest()
        {
            //Arrange
            RetweetComment retweetComment = new RetweetComment();
            retweetComment.CommentContent = "Comemnt";
            retweetComment.CommentDateTime = DateTime.Now;
            retweetComment.UserId = 1;
            retweetComment.RetweetId = 1;
            var result = await retweetCommentRepository.Add(retweetComment);

            var AddedTweet = await retweetCommentRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.UserId);
        }

        [Test]
        public async Task GetRetweetCommentbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetCommentFoundException>(async () => await retweetCommentRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such Retweet Comment Found!", exception.Message);
        }


        [Test]
        public async Task GetAllRetweetCommentSuccessTest()
        {
            //Arrange
            var result = await retweetCommentRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllRetweetCommentFailTest()
        {
            //Arrange
            var result = await retweetCommentRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }



        [Test]
        public async Task UpdateRetweetCommentSuccessTest()
        {
            //Arrange
            RetweetComment retweetComment = new RetweetComment();
            retweetComment.CommentContent = "Comemnt";
            retweetComment.CommentDateTime = DateTime.Now;
            retweetComment.UserId = 1;
            retweetComment.RetweetId = 1;
            var result = await retweetCommentRepository.Add(retweetComment);

            var AddedTweet = await retweetCommentRepository.GetbyKey(result.Id);
            AddedTweet.RetweetId = 11;

            var UpdatedResult = await retweetCommentRepository.Update(result);

            //Assert
            Assert.AreEqual(11, result.RetweetId);
        }

        [Test]
        public async Task UpdateRetweetCommentFailTest()
        {
            //Arrange
            RetweetComment retweetComment = new RetweetComment();
            retweetComment.CommentContent = "Comemnt";
            retweetComment.CommentDateTime = DateTime.Now;
            retweetComment.UserId = 1;
            retweetComment.RetweetId = 1;
            var result = await retweetCommentRepository.Add(retweetComment);

            var AddedTweet = await retweetCommentRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetCommentFoundException>(async () => await retweetCommentRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such Retweet Comment Found!", exception.Message);
        }
    }
}
