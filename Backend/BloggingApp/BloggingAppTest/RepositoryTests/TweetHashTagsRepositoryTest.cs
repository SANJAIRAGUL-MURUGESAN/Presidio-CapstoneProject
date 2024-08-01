using BloggingApp.Contexts;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Exceptions.TweetHashTagsExceptions;
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
    public class TweetHashTagsRepositoryTest
    {
        BloggingAppContext context;
        TweetHashTagsRepository tweetHashTagsRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            tweetHashTagsRepository = new TweetHashTagsRepository(context);
        }
        [Test]
        public async Task AddTweetHashTagsSuccessTest()
        {
            //Arrange


            TweetHashTags tweetHashTags = new TweetHashTags();
            tweetHashTags.TweetId = 1;
            tweetHashTags.HashTagTitle = "hashtag";
            var result = await tweetHashTagsRepository.Add(tweetHashTags);
            //Assert
            Assert.AreEqual(1, result.TweetId);
        }

        [Test]
        public async Task DeleteTweetHashtagSuccessTest()
        {
            //Arrange
            TweetHashTags tweetHashTags = new TweetHashTags();
            tweetHashTags.TweetId = 1;
            tweetHashTags.HashTagTitle = "hashtag";
            var result = await tweetHashTagsRepository.Add(tweetHashTags);

            var DeletedTweet = await tweetHashTagsRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.TweetId);
        }


        [Test]
        public async Task TweetHashTagDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetHashTagsFoundException>(async () => await tweetHashTagsRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such Tweet HashTags Found!", exception.Message);
        }

        [Test]
        public async Task GetTweetHashtagByKeySuccessTest()
        {
            //Arrange
            TweetHashTags tweetHashTags = new TweetHashTags();
            tweetHashTags.TweetId = 1;
            tweetHashTags.HashTagTitle = "hashtag";
            var result = await tweetHashTagsRepository.Add(tweetHashTags);

            var AddedTweet = await tweetHashTagsRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.TweetId);
        }


        [Test]
        public async Task GetTweetHashtagbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetHashTagsFoundException>(async () => await tweetHashTagsRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such Tweet HashTags Found!", exception.Message);
        }

        [Test]
        public async Task GetAllTweetHasghtagsSuccessTest()
        {
            //Arrange
            var result = await tweetHashTagsRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTweetHashtagsFailTest()
        {
            //Arrange
            var result = await tweetHashTagsRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTweetHashtagsSuccessTest()
        {
            //Arrange
            TweetHashTags tweetHashTags = new TweetHashTags();
            tweetHashTags.TweetId = 1;
            tweetHashTags.HashTagTitle = "hashtag";
            var result = await tweetHashTagsRepository.Add(tweetHashTags);

            var AddedTweet = await tweetHashTagsRepository.GetbyKey(result.Id);
            AddedTweet.TweetId = 9;

            var UpdatedResult = await tweetHashTagsRepository.Update(result);

            //Assert
            Assert.AreEqual(9, result.TweetId);
        }

        [Test]
        public async Task UpdateTweetHashtgaFailTest()
        {
            //Arrange
            TweetHashTags tweetHashTags = new TweetHashTags();
            tweetHashTags.TweetId = 1;
            tweetHashTags.HashTagTitle = "hashtag";
            var result = await tweetHashTagsRepository.Add(tweetHashTags);

            var AddedTweet = await tweetHashTagsRepository.GetbyKey(result.Id);
            AddedTweet.TweetId = 9;
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchTweetHashTagsFoundException>(async () => await tweetHashTagsRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such Tweet HashTags Found!", exception.Message);
        }


    }
}
