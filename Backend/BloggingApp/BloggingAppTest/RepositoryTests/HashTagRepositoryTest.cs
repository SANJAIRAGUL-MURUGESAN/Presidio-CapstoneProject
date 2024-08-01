using BloggingApp.Contexts;
using BloggingApp.Exceptions.HashTagsExceptions;
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
    public class HashTagRepositoryTest
    {
        BloggingAppContext context;
        HashTagRepository hashTagRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            hashTagRepository = new HashTagRepository(context);
        }

        [Test]
        public async Task AddHashTagSuccessTest()
        {
            //Arrange

            HashTags hashtag1 = new HashTags();
            hashtag1.HashTagTitle = "Hastag";
            hashtag1.CountInPosts = 1;
            hashtag1.CountInComments = 0;
            hashtag1.TweetLikes = 0;
            hashtag1.HashTagCreatedDateTime = DateTime.Now;
            hashtag1.UserId = 1;
            var result = await hashTagRepository.Add(hashtag1);

            //Assert
            Assert.AreEqual(1, result.UserId);
        }

        [Test]
        public async Task DeleteHashTagSuccessTest()
        {
            //Arrange

            HashTags hashtag1 = new HashTags();
            hashtag1.HashTagTitle = "Hastag";
            hashtag1.CountInPosts = 1;
            hashtag1.CountInComments = 0;
            hashtag1.TweetLikes = 0;
            hashtag1.HashTagCreatedDateTime = DateTime.Now;
            hashtag1.UserId = 1;
            var result = await hashTagRepository.Add(hashtag1);

            var DeletedTweet = await hashTagRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.UserId);
        }


        [Test]
        public async Task HashTagDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchHashTagFoundException>(async () => await hashTagRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such hashTag Found!", exception.Message);
        }

        [Test]
        public async Task GetHashTagtByKeySuccessTest()
        {
            //Arrange
            HashTags hashtag1 = new HashTags();
            hashtag1.HashTagTitle = "Hastag";
            hashtag1.CountInPosts = 1;
            hashtag1.CountInComments = 0;
            hashtag1.TweetLikes = 0;
            hashtag1.HashTagCreatedDateTime = DateTime.Now;
            hashtag1.UserId = 1;
            var result = await hashTagRepository.Add(hashtag1);

            var AddedTweet = await hashTagRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.UserId);
        }


        [Test]
        public async Task GethashtagbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchHashTagFoundException>(async () => await hashTagRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such hashTag Found!", exception.Message);
        }

        [Test]
        public async Task GetAllHashTagSuccessTest()
        {
            //Arrange
            var result = await hashTagRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllHashTagFailTest()
        {
            //Arrange
            var result = await hashTagRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateHashTagSuccessTest()
        {
            //Arrange
            HashTags hashtag1 = new HashTags();
            hashtag1.HashTagTitle = "Hastag";
            hashtag1.CountInPosts = 1;
            hashtag1.CountInComments = 0;
            hashtag1.TweetLikes = 0;
            hashtag1.HashTagCreatedDateTime = DateTime.Now;
            hashtag1.UserId = 1;
            var result = await hashTagRepository.Add(hashtag1);

            var AddedTweet = await hashTagRepository.GetbyKey(result.Id);
            AddedTweet.UserId = 111;

            var UpdatedResult = await hashTagRepository.Update(result);

            //Assert
            Assert.AreEqual(111, result.UserId);
        }

        [Test]
        public async Task UpdateHashtagFailTest()
        {
            //Arrange
            HashTags hashtag1 = new HashTags();
            hashtag1.HashTagTitle = "Hastag";
            hashtag1.CountInPosts = 1;
            hashtag1.CountInComments = 0;
            hashtag1.TweetLikes = 0;
            hashtag1.HashTagCreatedDateTime = DateTime.Now;
            hashtag1.UserId = 1;
            var result = await hashTagRepository.Add(hashtag1);


            var AddedTweet = await hashTagRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchHashTagFoundException>(async () => await hashTagRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such hashTag Found!", exception.Message);
        }
    }
}
