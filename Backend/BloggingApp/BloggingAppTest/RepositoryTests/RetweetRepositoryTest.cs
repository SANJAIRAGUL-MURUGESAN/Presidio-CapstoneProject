using BloggingApp.Contexts;
using BloggingApp.Exceptions.RetweetExceptions;
using BloggingApp.Exceptions.TweetExceptions;
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
    public class RetweetRepositoryTest
    {
        BloggingAppContext context;
        RetweetRepository retweetRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            retweetRepository = new RetweetRepository(context);
        }

        [Test]
        public async Task AddRetweetSuccessTest()
        {
            //Arrange

            Retweet retweet = new Retweet();
            retweet.RetweetContent = "Happy Coding";
            retweet.RetweetDateTime = DateTime.Now;
            retweet.IsCommentEnable = "Yes";
            retweet.ActualTweetId = 1;
            retweet.UserId = 1;
            var result = await retweetRepository.Add(retweet);

            //Assert
            Assert.AreEqual(1, result.UserId);
        }


        [Test]
        public async Task DeleteRetweetSuccessTest()
        {
            //Arrange
            Retweet retweet = new Retweet();
            retweet.RetweetContent = "Happy Coding";
            retweet.RetweetDateTime = DateTime.Now;
            retweet.IsCommentEnable = "Yes";
            retweet.ActualTweetId = 1;
            retweet.UserId = 1;
            var result = await retweetRepository.Add(retweet);

            var DeletedTweet = await retweetRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.UserId);
        }

        [Test]
        public async Task RetweetDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetFoundException>(async () => await retweetRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such Retweet Found!", exception.Message);
        }

        [Test]
        public async Task GetRetweetByKeySuccessTest()
        {
            //Arrange
            Retweet retweet = new Retweet();
            retweet.RetweetContent = "Happy Coding";
            retweet.RetweetDateTime = DateTime.Now;
            retweet.IsCommentEnable = "Yes";
            retweet.ActualTweetId = 1;
            retweet.UserId = 1;
            var result = await retweetRepository.Add(retweet);

            var AddedTweet = await retweetRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.UserId);
        }


        [Test]
        public async Task GetRetweetbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetFoundException>(async () => await retweetRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such Retweet Found!", exception.Message);
        }

        [Test]
        public async Task GetAllRetweetsSuccessTest()
        {
            //Arrange
            var result = await retweetRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllUsersFailTest()
        {
            //Arrange
            var result = await retweetRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateRweetSuccessTest()
        {
            //Arrange
            Retweet retweet = new Retweet();
            retweet.RetweetContent = "Happy Coding";
            retweet.RetweetDateTime = DateTime.Now;
            retweet.IsCommentEnable = "Yes";
            retweet.ActualTweetId = 1;
            retweet.UserId = 1;
            var result = await retweetRepository.Add(retweet);

            var AddedTweet = await retweetRepository.GetbyKey(result.Id);
            AddedTweet.IsCommentEnable = "No";

            var UpdatedResult = await retweetRepository.Update(result);

            //Assert
            Assert.AreEqual("No", result.IsCommentEnable);
        }

        [Test]
        public async Task UpdateRetweetFailTest()
        {
            //Arrange
            Retweet retweet = new Retweet();
            retweet.RetweetContent = "Happy Coding";
            retweet.RetweetDateTime = DateTime.Now;
            retweet.IsCommentEnable = "Yes";
            retweet.ActualTweetId = 1;
            retweet.UserId = 1;
            var result = await retweetRepository.Add(retweet);

            var AddedTweet = await retweetRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchRetweetFoundException>(async () => await retweetRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such Retweet Found!", exception.Message);
        }

    }
}
