using BloggingApp.Contexts;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Exceptions.UserExceptions;
using BloggingApp.Models;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAppTest.RepositoryTests
{
    public class TweetRepositoryTest
    {
        BloggingAppContext context;
        TweetRepository tweetRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            tweetRepository = new TweetRepository(context);
        }


        [Test]
        public async Task AddTweetSuccessTest()
        {
            //Arrange

            Tweet tweet = new Tweet();
            tweet.TweetContent = "Hi gayathri! Happy Coding!";
            tweet.TweetDateTime = DateTime.Now;
            tweet.IsCommentEnable = "Yes";
            tweet.UserId = 1;
            var result = await tweetRepository.Add(tweet);

            //Assert
            Assert.AreEqual(1, result.UserId);
        }

        [Test]
        public async Task DeleteTweetSuccessTest()
        {
            //Arrange
            Tweet tweet = new Tweet();
            tweet.TweetContent = "Hi gayathri! Happy Coding!";
            tweet.TweetDateTime = DateTime.Now;
            tweet.IsCommentEnable = "Yes";
            tweet.UserId = 1;
            var result = await tweetRepository.Add(tweet);

            var DeletedTweet = await tweetRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.UserId);
        }

        [Test]
        public async Task TweetDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetFoundException>(async () => await tweetRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such Tweet Found!", exception.Message);
        }

        [Test]
        public async Task GetTweetByKeySuccessTest()
        {
            //Arrange
            Tweet tweet = new Tweet();
            tweet.TweetContent = "Hi gayathri! Happy Coding!";
            tweet.TweetDateTime = DateTime.Now;
            tweet.IsCommentEnable = "Yes";
            tweet.UserId = 1;
            var result = await tweetRepository.Add(tweet);

            var AddedTweet = await tweetRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.UserId);
        }

        [Test]
        public async Task GetTweetbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetFoundException>(async () => await tweetRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such Tweet Found!", exception.Message);
        }

        [Test]
        public async Task GetAllTweetsSuccessTest()
        {
            //Arrange
            var result = await tweetRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTweetsFailTest()
        {
            //Arrange
            var result = await tweetRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task UpdateTweetSuccessTest()
        {
            //Arrange
            Tweet tweet = new Tweet();
            tweet.TweetContent = "Hi gayathri! Happy Coding!";
            tweet.TweetDateTime = DateTime.Now;
            tweet.IsCommentEnable = "Yes";
            tweet.UserId = 1;
            var result = await tweetRepository.Add(tweet);

            var AddedTweet = await tweetRepository.GetbyKey(result.Id);
            AddedTweet.IsCommentEnable = "No";

            var UpdatedResult = await tweetRepository.Update(result);

            //Assert
            Assert.AreEqual("No", result.IsCommentEnable);
        }

        [Test]
        public async Task UpdateTweetFailTest()
        {
            //Arrange
            Tweet tweet = new Tweet();
            tweet.TweetContent = "Hi gayathri! Happy Coding!";
            tweet.TweetDateTime = DateTime.Now;
            tweet.IsCommentEnable = "Yes";
            tweet.UserId = 1;
            var result = await tweetRepository.Add(tweet);

            var AddedTweet = await tweetRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchTweetFoundException>(async () => await tweetRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such Tweet Found!", exception.Message);
        }

    }
}
