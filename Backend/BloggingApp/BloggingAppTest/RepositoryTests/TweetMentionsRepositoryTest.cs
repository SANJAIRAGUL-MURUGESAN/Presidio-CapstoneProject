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
    public class TweetMentionsRepositoryTest
    {
        BloggingAppContext context;
        TweetMentionsRepository tweetMentionsRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            tweetMentionsRepository = new TweetMentionsRepository(context);
        }

        [Test]
        public async Task AddTweetMentionsSuccessTest()
        {
            //Arrange

            TweetMentions tweetMentions = new TweetMentions();
            tweetMentions.MentionerId = 1;
            tweetMentions.MentionedByUserId = 1;
            tweetMentions.TweetId = 1;
            tweetMentions.MentionedDateTime = DateTime.Now;
            var result = await tweetMentionsRepository.Add(tweetMentions);

            //Assert
            Assert.AreEqual(1, result.TweetId);
        }

        [Test]
        public async Task DeleteTweetMentionsSuccessTest()
        {
            //Arrange
            TweetMentions tweetMentions = new TweetMentions();
            tweetMentions.MentionerId = 1;
            tweetMentions.MentionedByUserId = 1;
            tweetMentions.TweetId = 1;
            tweetMentions.MentionedDateTime = DateTime.Now;
            var result = await tweetMentionsRepository.Add(tweetMentions);

            var DeletedTweet = await tweetMentionsRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.TweetId);
        }

        [Test]
        public async Task TweetMentionsDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetMentionFoundException>(async () => await tweetMentionsRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such Tweet Mentions Found!", exception.Message);
        }

        [Test]
        public async Task GetTweetMentioonssByKeySuccessTest()
        {
            //Arrange
            TweetMentions tweetMentions = new TweetMentions();
            tweetMentions.MentionerId = 1;
            tweetMentions.MentionedByUserId = 1;
            tweetMentions.TweetId = 1;
            tweetMentions.MentionedDateTime = DateTime.Now;
            var result = await tweetMentionsRepository.Add(tweetMentions);

            var AddedTweet = await tweetMentionsRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.TweetId);
        }

        [Test]
        public async Task GetTweetMentionsbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetMentionFoundException>(async () => await tweetMentionsRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such Tweet Mentions Found!", exception.Message);
        }


        [Test]
        public async Task GetAllTweetMentionsSuccessTest()
        {
            //Arrange
            var result = await tweetMentionsRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTweetMentionsFailTest()
        {
            //Arrange
            var result = await tweetMentionsRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTweetMentionsSuccessTest()
        {
            //Arrange
            TweetMentions tweetMentions = new TweetMentions();
            tweetMentions.MentionerId = 1;
            tweetMentions.MentionedByUserId = 1;
            tweetMentions.TweetId = 1;
            tweetMentions.MentionedDateTime = DateTime.Now;
            var result = await tweetMentionsRepository.Add(tweetMentions);


            var AddedTweet = await tweetMentionsRepository.GetbyKey(result.Id);
            AddedTweet.MentionerId = 2;

            var UpdatedResult = await tweetMentionsRepository.Update(result);

            //Assert
            Assert.AreEqual(2, result.MentionerId);
        }

        [Test]
        public async Task UpdateTweetMentionsFailTest()
        {
            //Arrange
            TweetMentions tweetMentions = new TweetMentions();
            tweetMentions.MentionerId = 1;
            tweetMentions.MentionedByUserId = 1;
            tweetMentions.TweetId = 1;
            tweetMentions.MentionedDateTime = DateTime.Now;
            var result = await tweetMentionsRepository.Add(tweetMentions);

            var AddedTweet = await tweetMentionsRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchTweetMentionFoundException>(async () => await tweetMentionsRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such Tweet Mentions Found!", exception.Message);
        }


    }
}
