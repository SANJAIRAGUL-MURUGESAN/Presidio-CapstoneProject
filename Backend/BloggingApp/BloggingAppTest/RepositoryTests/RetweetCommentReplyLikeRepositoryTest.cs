using BloggingApp.Contexts;
using BloggingApp.Exceptions.RetweetCommentreplyLikesException;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Models;
using BloggingApp.Models.CRLikesDTOs;
using BloggingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAppTest.RepositoryTests
{
    public class RetweetCommentReplyLikeRepositoryTest
    {
        BloggingAppContext context;
        RetweetCommentReplyLikesRepository RetweetCommentReplyLikesRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            RetweetCommentReplyLikesRepository = new RetweetCommentReplyLikesRepository(context);
        }

        [Test]
        public async Task AddRetweetCommentReplyLikeSuccessTest()
        {
            //Arrange

            RetweetCommentReplyLikes retweetCommentReplyLikes = new RetweetCommentReplyLikes();
            retweetCommentReplyLikes.LikedUserId = 1;
            retweetCommentReplyLikes.ReplyCommentReplyId = 1;
            var result = await RetweetCommentReplyLikesRepository.Add(retweetCommentReplyLikes);

            //Assert
            Assert.AreEqual(1, result.LikedUserId);
        }


        [Test]
        public async Task DeleteRetweetCommentReplyLikeSuccessTest()
        {
            //Arrange
            RetweetCommentReplyLikes retweetCommentReplyLikes = new RetweetCommentReplyLikes();
            retweetCommentReplyLikes.LikedUserId = 1;
            retweetCommentReplyLikes.ReplyCommentReplyId = 1;
            var result = await RetweetCommentReplyLikesRepository.Add(retweetCommentReplyLikes);

            var DeletedTweet = await RetweetCommentReplyLikesRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.LikedUserId);
        }
        [Test]
        public async Task RetweetCommentReplyLikeDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetCommentReplyLikeFoundException>(async () => await RetweetCommentReplyLikesRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Retweet Comment Reply Like Found!", exception.Message);
        }

        [Test]
        public async Task GetRetweetCommentReplyLikeByKeySuccessTest()
        {
            //Arrange
            //Arrange
            RetweetCommentReplyLikes retweetCommentReplyLikes = new RetweetCommentReplyLikes();
            retweetCommentReplyLikes.LikedUserId = 1;
            retweetCommentReplyLikes.ReplyCommentReplyId = 1;
            var result = await RetweetCommentReplyLikesRepository.Add(retweetCommentReplyLikes);

            var AddedTweet = await RetweetCommentReplyLikesRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.LikedUserId);
        }

        [Test]
        public async Task GetRetweetCommentReplybyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetCommentReplyLikeFoundException>(async () => await RetweetCommentReplyLikesRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Retweet Comment Reply Like Found!", exception.Message);
        }

        [Test]
        public async Task GetAllRetweetCommentReplyLikeSuccessTest()
        {
            //Arrange
            var result = await RetweetCommentReplyLikesRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllRetweetCommentReplyLikeFailTest()
        {
            //Arrange
            var result = await RetweetCommentReplyLikesRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task UpdateRetweetCommentReplyLikeSuccessTest()
        {
            //Arrange
            RetweetCommentReplyLikes retweetCommentReplyLikes = new RetweetCommentReplyLikes();
            retweetCommentReplyLikes.LikedUserId = 1;
            retweetCommentReplyLikes.ReplyCommentReplyId = 1;
            var result = await RetweetCommentReplyLikesRepository.Add(retweetCommentReplyLikes);

            var AddedTweet = await RetweetCommentReplyLikesRepository.GetbyKey(result.Id);
            AddedTweet.LikedUserId = 111;

            var UpdatedResult = await RetweetCommentReplyLikesRepository.Update(result);

            //Assert
            Assert.AreEqual(111, result.LikedUserId);
        }

        [Test]
        public async Task UpdateRetweetCommentReplyLikeFailTest()
        {
            //Arrange
            RetweetCommentReplyLikes retweetCommentReplyLikes = new RetweetCommentReplyLikes();
            retweetCommentReplyLikes.LikedUserId = 1;
            retweetCommentReplyLikes.ReplyCommentReplyId = 1;
            var result = await RetweetCommentReplyLikesRepository.Add(retweetCommentReplyLikes);

            var AddedTweet = await RetweetCommentReplyLikesRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetCommentReplyLikeFoundException>(async () => await RetweetCommentReplyLikesRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Retweet Comment Reply Like Found!", exception.Message);
        }


    }
}
