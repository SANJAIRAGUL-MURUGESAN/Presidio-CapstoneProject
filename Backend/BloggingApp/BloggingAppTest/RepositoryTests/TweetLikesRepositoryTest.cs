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
using BloggingApp.Exceptions.TweetLikesRepository;

namespace BloggingAppTest.RepositoryTests
{
    public class TweetLikesRepositoryTest
    {
        BloggingAppContext context;
        TweetLikesRepository tweetLikesRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            tweetLikesRepository = new TweetLikesRepository(context);

        }


        [Test]
        public async Task AddTweetLikesSuccessTest()
        {
            //Arrange

            TweetLikes tweetLikes = new TweetLikes();
            tweetLikes.LikedUserId = 1;
            tweetLikes.TweetId = 1;
            var result = await tweetLikesRepository.Add(tweetLikes);

            //Assert
            Assert.AreEqual(1, result.TweetId);
        }

        [Test]
        public async Task DeleteTweetLikeSuccessTest()
        {

            //Arrange
            TweetLikes tweetLikes = new TweetLikes();
            tweetLikes.LikedUserId = 1;
            tweetLikes.TweetId = 1;
            var result = await tweetLikesRepository.Add(tweetLikes);

            var DeletedTweet = await tweetLikesRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.TweetId);
        }


        [Test]
        public async Task TweetDeleteLikeFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetLikeFoundException>(async () => await tweetLikesRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such Tweet Like Found!", exception.Message);
        }

        [Test]
        public async Task GetTweetLikeByKeySuccessTest()
        {
            //Arrange
            TweetLikes tweetLikes = new TweetLikes();
            tweetLikes.LikedUserId = 1;
            tweetLikes.TweetId = 1;
            var result = await tweetLikesRepository.Add(tweetLikes);

            var AddedTweet = await tweetLikesRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.TweetId);
        }

        [Test]
        public async Task GetTweetLikebyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetLikeFoundException>(async () => await tweetLikesRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such Tweet Like Found!", exception.Message);
        }

        [Test]
        public async Task GetAllTweetLikesSuccessTest()
        {
            //Arrange
            var result = await tweetLikesRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTweetsLikesFailTest()
        {
            //Arrange
            var result = await tweetLikesRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTweetLikesSuccessTest()
        {
            //Arrange
            TweetLikes tweetLikes = new TweetLikes();
            tweetLikes.LikedUserId = 1;
            tweetLikes.TweetId = 1;
            var result = await tweetLikesRepository.Add(tweetLikes);

            var AddedTweet = await tweetLikesRepository.GetbyKey(result.Id);

            AddedTweet.TweetId = 3;

            var UpdatedResult = await tweetLikesRepository.Update(result);

            //Assert
            Assert.AreEqual(3, result.TweetId);
        }

        [Test]
        public async Task UpdateTweetLikeFailTest()
        {
            //Arrange
            TweetLikes tweetLikes = new TweetLikes();
            tweetLikes.LikedUserId = 1;
            tweetLikes.TweetId = 1;
            var result = await tweetLikesRepository.Add(tweetLikes);

            var AddedTweet = await tweetLikesRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            //Action
            var exception = Assert.ThrowsAsync<NoSuchTweetLikeFoundException>(async () => await tweetLikesRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such Tweet Like Found!", exception.Message);
        }


    }
}
