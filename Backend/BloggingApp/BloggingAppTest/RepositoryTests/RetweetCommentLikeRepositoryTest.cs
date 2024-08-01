using BloggingApp.Contexts;
using BloggingApp.Exceptions.RetweetCommentLikesExceptions;
using BloggingApp.Exceptions.RetweetCommentreplyLikesException;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Models;
using BloggingApp.Models.NewFolder;
using BloggingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAppTest.RepositoryTests
{
    public class RetweetCommentLikeRepositoryTest
    {
        BloggingAppContext context;
        RetweetCommentLikesRepository RetweetCommentLikesRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            RetweetCommentLikesRepository = new RetweetCommentLikesRepository(context);
        }

        [Test]
        public async Task AddRetweetCommentLikesSuccessTest()
        {
            //Arrange

            RetweetCommentLikes retweetCommentLikes = new RetweetCommentLikes();
            retweetCommentLikes.LikedUserId = 1;
            retweetCommentLikes.RetweetCommentId = 1;
            var result = await RetweetCommentLikesRepository.Add(retweetCommentLikes);

            //Assert
            Assert.AreEqual(1, result.LikedUserId);
        }

        [Test]
        public async Task DeleteRetweetCommentLikesSuccessTest()
        {
            //Arrange
            RetweetCommentLikes retweetCommentLikes = new RetweetCommentLikes();
            retweetCommentLikes.LikedUserId = 1;
            retweetCommentLikes.RetweetCommentId = 1;
            var result = await RetweetCommentLikesRepository.Add(retweetCommentLikes);

            var DeletedTweet = await RetweetCommentLikesRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.LikedUserId);
        }

        [Test]
        public async Task RetweetCommentLikesDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetCommentLikeFoundException>(async () => await RetweetCommentLikesRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such Retweet Comment Like Found!", exception.Message);
        }

        [Test]
        public async Task GetRetweetCommentLikesByKeySuccessTest()
        {
            //Arrange
            RetweetCommentLikes retweetCommentLikes = new RetweetCommentLikes();
            retweetCommentLikes.LikedUserId = 1;
            retweetCommentLikes.RetweetCommentId = 1;
            var result = await RetweetCommentLikesRepository.Add(retweetCommentLikes);

            var AddedTweet = await RetweetCommentLikesRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.LikedUserId);
        }

        [Test]
        public async Task GetRetweetCommentLikesbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetCommentLikeFoundException>(async () => await RetweetCommentLikesRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such Retweet Comment Like Found!", exception.Message);
        }

        [Test]
        public async Task GetAllRetweetCommentLikesSuccessTest()
        {
            //Arrange
            var result = await RetweetCommentLikesRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllRetweetCommentLikesFailTest()
        {
            //Arrange
            var result = await RetweetCommentLikesRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task UpdateRetweetCommentLikesSuccessTest()
        {
            //Arrange
            RetweetCommentLikes retweetCommentLikes = new RetweetCommentLikes();
            retweetCommentLikes.LikedUserId = 1;
            retweetCommentLikes.RetweetCommentId = 1;
            var result = await RetweetCommentLikesRepository.Add(retweetCommentLikes);

            var AddedTweet = await RetweetCommentLikesRepository.GetbyKey(result.Id);
            AddedTweet.LikedUserId = 1;

            var UpdatedResult = await RetweetCommentLikesRepository.Update(result);

            //Assert
            Assert.AreEqual(1, result.LikedUserId);
        }

        [Test]
        public async Task UpdateTweetFailTest()
        {
            //Arrange
            RetweetCommentLikes retweetCommentLikes = new RetweetCommentLikes();
            retweetCommentLikes.LikedUserId = 1;
            retweetCommentLikes.RetweetCommentId = 1;
            var result = await RetweetCommentLikesRepository.Add(retweetCommentLikes);

            var AddedTweet = await RetweetCommentLikesRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetCommentLikeFoundException>(async () => await RetweetCommentLikesRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such Retweet Comment Like Found!", exception.Message);
        }

    }
}
