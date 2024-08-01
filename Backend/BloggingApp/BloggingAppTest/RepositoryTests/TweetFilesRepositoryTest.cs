using BloggingApp.Contexts;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Exceptions.TweetFilesExceptions;
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
    public class TweetFilesRepositoryTest
    {
        BloggingAppContext context;
        TweetFilesRepository tweetFilesRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            tweetFilesRepository = new TweetFilesRepository(context);
        }

        [Test]
        public async Task AddTweetFilesSuccessTest()
        {
            //Arrange

            TweetFiles tweetFiles = new TweetFiles();
            tweetFiles.File1 = "File1";
            tweetFiles.File2 = "File2";
            tweetFiles.TweetId = 1;
            var result = await tweetFilesRepository.Add(tweetFiles);

            //Assert
            Assert.AreEqual(1, result.TweetId);
        }

        [Test]
        public async Task DeleteTweetFilesSuccessTest()
        {
            //Arrange
            TweetFiles tweetFiles = new TweetFiles();
            tweetFiles.File1 = "File1";
            tweetFiles.File2 = "File2";
            tweetFiles.TweetId = 1;
            var result = await tweetFilesRepository.Add(tweetFiles); 

            var DeletedTweet = await tweetFilesRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.TweetId);
        }

        [Test]
        public async Task TweetFilesDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetFileFoundException>(async () => await tweetFilesRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such Tweet File Found!", exception.Message);
        }

        [Test]
        public async Task GetTweetFilesByKeySuccessTest()
        {
            //Arrange
            TweetFiles tweetFiles = new TweetFiles();
            tweetFiles.File1 = "File1";
            tweetFiles.File2 = "File2";
            tweetFiles.TweetId = 1;
            var result = await tweetFilesRepository.Add(tweetFiles);

            var AddedTweet = await tweetFilesRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.TweetId);
        }

        [Test]
        public async Task GetTweetFilesbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetFileFoundException>(async () => await tweetFilesRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such Tweet File Found!", exception.Message);
        }

        [Test]
        public async Task GetAllTweetFilesSuccessTest()
        {
            //Arrange
            var result = await tweetFilesRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTweetFilesFailTest()
        {
            //Arrange
            var result = await tweetFilesRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTweetFilesSuccessTest()
        {
            //Arrange
            TweetFiles tweetFiles = new TweetFiles();
            tweetFiles.File1 = "File1";
            tweetFiles.File2 = "File2";
            tweetFiles.TweetId = 1;
            var result = await tweetFilesRepository.Add(tweetFiles);

            var AddedTweet = await tweetFilesRepository.GetbyKey(result.Id);
            AddedTweet.TweetId = 10;

            var UpdatedResult = await tweetFilesRepository.Update(result);

            //Assert
            Assert.AreEqual(10, result.TweetId);
        }

        [Test]
        public async Task UpdateTweetFilesFailTest()
        {
            //Arrange
            TweetFiles tweetFiles = new TweetFiles();
            tweetFiles.File1 = "File1";
            tweetFiles.File2 = "File2";
            tweetFiles.TweetId = 1;
            var result = await tweetFilesRepository.Add(tweetFiles);

            var AddedTweet = await tweetFilesRepository.GetbyKey(result.Id);
            AddedTweet.TweetId = 10;

            var UpdatedResult = await tweetFilesRepository.Update(result);
            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchTweetFileFoundException>(async () => await tweetFilesRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such Tweet File Found!", exception.Message);
        }


    }
}
