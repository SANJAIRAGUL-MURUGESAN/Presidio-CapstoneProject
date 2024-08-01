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
using BloggingApp.Exceptions.HashTagsExceptions;

namespace BloggingAppTest.RepositoryTests
{
    public class RetweetHashTagsRepositoryTest
    {
        BloggingAppContext context;
        RetweetHashTagRepository retweetHashTagRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            retweetHashTagRepository = new RetweetHashTagRepository(context);
        }

        [Test]
        public async Task AddRetweetHashTagsSuccessTest()
        {
            //Arrange

            RetweetHashTags retweetHashTags = new RetweetHashTags();
            retweetHashTags.RetweetId = 1;
            retweetHashTags.HashTagTitle = "hashta";
            var result = await retweetHashTagRepository.Add(retweetHashTags);

            //Assert
            Assert.AreEqual(1, result.RetweetId);
        }

        [Test]
        public async Task DeleteRetweetHashTagsSuccessTest()
        {
            //Arrange

            RetweetHashTags retweetHashTags = new RetweetHashTags();
            retweetHashTags.RetweetId = 1;
            retweetHashTags.HashTagTitle = "hashta";
            var result = await retweetHashTagRepository.Add(retweetHashTags);

            var DeletedTweet = await retweetHashTagRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.RetweetId);
        }

        [Test]
        public async Task RetweetHashTagsDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchHashTagFoundException>(async () => await retweetHashTagRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such hashTag Found!", exception.Message);
        }

        [Test]
        public async Task GetRetweetHashTagsByKeySuccessTest()
        {
            //Arrange

            RetweetHashTags retweetHashTags = new RetweetHashTags();
            retweetHashTags.RetweetId = 1;
            retweetHashTags.HashTagTitle = "hashta";
            var result = await retweetHashTagRepository.Add(retweetHashTags);

            var AddedTweet = await retweetHashTagRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.RetweetId);
        }


        [Test]
        public async Task GetRetweetHashTagsbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchHashTagFoundException>(async () => await retweetHashTagRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such hashTag Found!", exception.Message);
        }

        [Test]
        public async Task GetAllRetweetHashTagsSuccessTest()
        {
            //Arrange
            var result = await retweetHashTagRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllRetweetHashTagsFailTest()
        {
            //Arrange
            var result = await retweetHashTagRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateRetweetHashTagsSuccessTest()
        {
            //Arrange
            RetweetHashTags retweetHashTags = new RetweetHashTags();
            retweetHashTags.RetweetId = 1;
            retweetHashTags.HashTagTitle = "hashta";
            var result = await retweetHashTagRepository.Add(retweetHashTags);


            var AddedTweet = await retweetHashTagRepository.GetbyKey(result.Id);
            AddedTweet.RetweetId = 10;

            var UpdatedResult = await retweetHashTagRepository.Update(result);

            //Assert
            Assert.AreEqual(10, result.RetweetId);
        }


        [Test]
        public async Task UpdateRetweetHashTagsFailTest()
        {
            //Arrange
            RetweetHashTags retweetHashTags = new RetweetHashTags();
            retweetHashTags.RetweetId = 1;
            retweetHashTags.HashTagTitle = "hashta";
            var result = await retweetHashTagRepository.Add(retweetHashTags);

            var AddedTweet = await retweetHashTagRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchHashTagFoundException>(async () => await retweetHashTagRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such hashTag Found!", exception.Message);
        }


    }
}
