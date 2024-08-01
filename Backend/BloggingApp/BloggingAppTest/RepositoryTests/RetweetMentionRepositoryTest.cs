using BloggingApp.Contexts;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Exceptions.TweetMentionsExceptions;
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
    public class RetweetMentionRepositoryTest
    {
        BloggingAppContext context;
        RetweetMentionRepository retweetMentionRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            retweetMentionRepository = new RetweetMentionRepository(context);
        }

        [Test]
        public async Task AddRetweetMentionSuccessTest()
        {
            //Arrange

            RetweetMentions retweetMentions = new RetweetMentions();
            retweetMentions.MentionerId = 1;
            retweetMentions.MentionedByUserId = 2;
            retweetMentions.RetweetId = 1;
            retweetMentions.MentionedDateTime = DateTime.Now;
            var result = await retweetMentionRepository.Add(retweetMentions);

            //Assert
            Assert.AreEqual(1, result.MentionerId);
        }

        [Test]
        public async Task DeleteRetweetMentionSuccessTest()
        {
            //Arrange
            RetweetMentions retweetMentions = new RetweetMentions();
            retweetMentions.MentionerId = 1;
            retweetMentions.MentionedByUserId = 2;
            retweetMentions.RetweetId = 1;
            retweetMentions.MentionedDateTime = DateTime.Now;
            var result = await retweetMentionRepository.Add(retweetMentions);


            var DeletedTweet = await retweetMentionRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.MentionerId);
        }

        [Test]
        public async Task RetweetMentionDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetMentionFoundException>(async () => await retweetMentionRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such Tweet Mentions Found!", exception.Message);
        }


        [Test]
        public async Task GetRetweetMentionByKeySuccessTest()
        {
            //Arrange
            RetweetMentions retweetMentions = new RetweetMentions();
            retweetMentions.MentionerId = 1;
            retweetMentions.MentionedByUserId = 2;
            retweetMentions.RetweetId = 1;
            retweetMentions.MentionedDateTime = DateTime.Now;
            var result = await retweetMentionRepository.Add(retweetMentions);

            var AddedTweet = await retweetMentionRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.MentionerId);
        }


        [Test]
        public async Task GetRetweetMentionbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetMentionFoundException>(async () => await retweetMentionRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such Tweet Mentions Found!", exception.Message);
        }

        [Test]
        public async Task GetAllRetweetMentionSuccessTest()
        {
            //Arrange
            var result = await retweetMentionRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllRetweetMentionFailTest()
        {
            //Arrange
            var result = await retweetMentionRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateRetweetMentionSuccessTest()
        {
            //Arrange
            RetweetMentions retweetMentions = new RetweetMentions();
            retweetMentions.MentionerId = 1;
            retweetMentions.MentionedByUserId = 2;
            retweetMentions.RetweetId = 1;
            retweetMentions.MentionedDateTime = DateTime.Now;
            var result = await retweetMentionRepository.Add(retweetMentions);

            var AddedTweet = await retweetMentionRepository.GetbyKey(result.Id);
            AddedTweet.MentionerId = 6;

            var UpdatedResult = await retweetMentionRepository.Update(result);

            //Assert
            Assert.AreEqual(6, result.MentionerId);
        }

        [Test]
        public async Task UpdateRetweetMentionFailTest()
        {
            //Arrange
            RetweetMentions retweetMentions = new RetweetMentions();
            retweetMentions.MentionerId = 1;
            retweetMentions.MentionedByUserId = 2;
            retweetMentions.RetweetId = 1;
            retweetMentions.MentionedDateTime = DateTime.Now;
            var result = await retweetMentionRepository.Add(retweetMentions);

            var AddedTweet = await retweetMentionRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchTweetMentionFoundException>(async () => await retweetMentionRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such Tweet Mentions Found!", exception.Message);
        }

    }
}
