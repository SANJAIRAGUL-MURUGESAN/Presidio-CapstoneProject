using BloggingApp.Contexts;
using BloggingApp.Exceptions.TweeCommentLikesRepository;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Models;
using BloggingApp.Models.NewFolder;
using BloggingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAppTest.RepositoryTests
{
    public class TweetCommentLikeRepositoryTest
    {
        BloggingAppContext context;
        TweetCommentLikeRepository tweetCommentLikeRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            tweetCommentLikeRepository = new TweetCommentLikeRepository(context);
        }

        [Test]
        public async Task AddTweetCommentLikeSuccessTest()
        {
            //Arrange

            TweetCommentLikes tweetCommentLikes = new TweetCommentLikes();
            tweetCommentLikes.LikedUserId = 1;
            tweetCommentLikes.CommentId = 1;
            var result = await tweetCommentLikeRepository.Add(tweetCommentLikes);

            //Assert
            Assert.AreEqual(1, result.CommentId);
        }


        [Test]
        public async Task DeleteTweetCommentLikeSuccessTest()
        {
            //Arrange
            TweetCommentLikes tweetCommentLikes = new TweetCommentLikes();
            tweetCommentLikes.LikedUserId = 1;
            tweetCommentLikes.CommentId = 1;
            var result = await tweetCommentLikeRepository.Add(tweetCommentLikes);

            var DeletedTweet = await tweetCommentLikeRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.CommentId);
        }

        [Test]
        public async Task TweetCommentLikeDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetCommentLikeFoundException>(async () => await tweetCommentLikeRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such TweetComment Like Found!", exception.Message);
        }

        [Test]
        public async Task GetTweetCommentLikeByKeySuccessTest()
        {
            //Arrange
            TweetCommentLikes tweetCommentLikes = new TweetCommentLikes();
            tweetCommentLikes.LikedUserId = 1;
            tweetCommentLikes.CommentId = 1;
            var result = await tweetCommentLikeRepository.Add(tweetCommentLikes);

            var AddedTweet = await tweetCommentLikeRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.CommentId);
        }


        [Test]
        public async Task GetTweetCommentLikebyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetCommentLikeFoundException>(async () => await tweetCommentLikeRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such TweetComment Like Found!", exception.Message);
        }


        [Test]
        public async Task GetAllTweetCommentLikeSuccessTest()
        {
            //Arrange
            var result = await tweetCommentLikeRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTweetCommentLikeFailTest()
        {
            //Arrange
            var result = await tweetCommentLikeRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task UpdateTweetCommentLikeSuccessTest()
        {
            //Arrange
            TweetCommentLikes tweetCommentLikes = new TweetCommentLikes();
            tweetCommentLikes.LikedUserId = 1;
            tweetCommentLikes.CommentId = 1;
            var result = await tweetCommentLikeRepository.Add(tweetCommentLikes);

            var AddedTweet = await tweetCommentLikeRepository.GetbyKey(result.Id);
            AddedTweet.CommentId = 1;

            var UpdatedResult = await tweetCommentLikeRepository.Update(result);

            //Assert
            Assert.AreEqual(1, result.CommentId);
        }

        [Test]
        public async Task UpdateTweetCommentLikeFailTest()
        {
            //Arrange
            TweetCommentLikes tweetCommentLikes = new TweetCommentLikes();
            tweetCommentLikes.LikedUserId = 1;
            tweetCommentLikes.CommentId = 1;
            var result = await tweetCommentLikeRepository.Add(tweetCommentLikes);

            var AddedTweet = await tweetCommentLikeRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchTweetCommentLikeFoundException>(async () => await tweetCommentLikeRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such TweetComment Like Found!", exception.Message);
        }

    }
}
