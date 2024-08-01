using BloggingApp.Contexts;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Models;
using BloggingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Exceptions.RetweetLikesExceptions;

namespace BloggingAppTest.RepositoryTests
{
    public class RetweetLikesRespositoryTest
    {
        BloggingAppContext context;
        RetweetLikesRepository retweetLikesRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            retweetLikesRepository = new RetweetLikesRepository(context);
        }

        [Test]
        public async Task AddRetweetLikeSuccessTest()
        {
            //Arrange

            RetweetLikes retweetLikes = new RetweetLikes();
            retweetLikes.LikedUserId = 1;
            retweetLikes.RetweetId = 1;
            var result = await retweetLikesRepository.Add(retweetLikes);

            //Assert
            Assert.AreEqual(1, result.LikedUserId);
        }

        [Test]
        public async Task DeleteRetweetLikeSuccessTest()
        {
            //Arrange

            RetweetLikes retweetLikes = new RetweetLikes();
            retweetLikes.LikedUserId = 1;
            retweetLikes.RetweetId = 1;
            var result = await retweetLikesRepository.Add(retweetLikes);

            var DeletedTweet = await retweetLikesRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.LikedUserId);
        }


        [Test]
        public async Task RetweetLikeDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetLikesFoundException>(async () => await retweetLikesRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such Retweet Like Found!", exception.Message);
        }


        [Test]
        public async Task GetRetweetLikeByKeySuccessTest()
        {
            //Arrange
            RetweetLikes retweetLikes = new RetweetLikes();
            retweetLikes.LikedUserId = 1;
            retweetLikes.RetweetId = 1;
            var result = await retweetLikesRepository.Add(retweetLikes);

            var AddedTweet = await retweetLikesRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.LikedUserId);
        }

        [Test]
        public async Task GetRetweetLikebyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetLikesFoundException>(async () => await retweetLikesRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such Retweet Like Found!", exception.Message);
        }


        [Test]
        public async Task GetAllRetweetLikeSuccessTest()
        {
            //Arrange
            var result = await retweetLikesRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllRetweetLikeFailTest()
        {
            //Arrange
            var result = await retweetLikesRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateRetweetLikeSuccessTest()
        {
            //Arrange
            RetweetLikes retweetLikes = new RetweetLikes();
            retweetLikes.LikedUserId = 1;
            retweetLikes.RetweetId = 1;
            var result = await retweetLikesRepository.Add(retweetLikes);

            var AddedTweet = await retweetLikesRepository.GetbyKey(result.Id);
            AddedTweet.LikedUserId =1;

            var UpdatedResult = await retweetLikesRepository.Update(result);

            //Assert
            Assert.AreEqual(1, result.LikedUserId);
        }
        
        [Test]
        public async Task UpdateRetweetLikeFailTest()
        {
            //Arrange
            //Arrange
            RetweetLikes retweetLikes = new RetweetLikes();
            retweetLikes.LikedUserId = 1;
            retweetLikes.RetweetId = 1;
            var result = await retweetLikesRepository.Add(retweetLikes);

            var AddedTweet = await retweetLikesRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetLikesFoundException>(async () => await retweetLikesRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such Retweet Like Found!", exception.Message);
        }


    }
}
