using BloggingApp.Contexts;
using BloggingApp.Exceptions.TweetCommentReplyLikesRepository;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Models;
using BloggingApp.Models.CRLikesDTOs;
using BloggingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAppTest.RepositoryTests
{
    public class TweetCommentReplyLikeRepositoryTest
    {
        BloggingAppContext context;
        TweetCommentReplyLikeRepository TweetCommentReplyLikeRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            TweetCommentReplyLikeRepository = new TweetCommentReplyLikeRepository(context);
        }

        [Test]
        public async Task AddTweetCommentReplyLikeSuccessTest()
        {
            //Arrange

            TweetReplyLikes tweetReplyLikes = new TweetReplyLikes();
            tweetReplyLikes.LikedUserId = 1;
            tweetReplyLikes.ReplyId = 1;
            var result = await TweetCommentReplyLikeRepository.Add(tweetReplyLikes);
            //Assert
            Assert.AreEqual(1, result.ReplyId);
        }

        [Test]
        public async Task DeleteTweetCommentReplyLikeSuccessTest()
        {
            //Arrange

            TweetReplyLikes tweetReplyLikes = new TweetReplyLikes();
            tweetReplyLikes.LikedUserId = 1;
            tweetReplyLikes.ReplyId = 1;
            var result = await TweetCommentReplyLikeRepository.Add(tweetReplyLikes);

            var DeletedTweet = await TweetCommentReplyLikeRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.ReplyId);
        }


        [Test]
        public async Task TweetCommentReplyLikeDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetCommentReplyLikeFoundException>(async () => await TweetCommentReplyLikeRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such TweetComment Reply Like Found!", exception.Message);
        }

        [Test]
        public async Task GetTweetCommentReplyLikeByKeySuccessTest()
        {
            //Arrange
            TweetReplyLikes tweetReplyLikes = new TweetReplyLikes();
            tweetReplyLikes.LikedUserId = 1;
            tweetReplyLikes.ReplyId = 1;
            var result = await TweetCommentReplyLikeRepository.Add(tweetReplyLikes);

            var AddedTweet = await TweetCommentReplyLikeRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.ReplyId);
        }

        [Test]
        public async Task GetTweetCommentReplyLikebyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchTweetCommentReplyLikeFoundException>(async () => await TweetCommentReplyLikeRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such TweetComment Reply Like Found!", exception.Message);
        }

        [Test]
        public async Task GetAllTweetCommentReplyLikeSuccessTest()
        {
            //Arrange
            var result = await TweetCommentReplyLikeRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTweetCommentReplyLikeFailTest()
        {
            //Arrange
            var result = await TweetCommentReplyLikeRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTweetCommentReplyLikeSuccessTest()
        {
            //Arrange
            TweetReplyLikes tweetReplyLikes = new TweetReplyLikes();
            tweetReplyLikes.LikedUserId = 1;
            tweetReplyLikes.ReplyId = 1;
            var result = await TweetCommentReplyLikeRepository.Add(tweetReplyLikes);

            var AddedTweet = await TweetCommentReplyLikeRepository.GetbyKey(result.Id);
            AddedTweet.ReplyId = 2;

            var UpdatedResult = await TweetCommentReplyLikeRepository.Update(result);

            //Assert
            Assert.AreEqual(2, result.ReplyId);
        }

        [Test]
        public async Task UpdateTweetCommentReplyLikeFailTest()
        {
            //Arrange
            TweetReplyLikes tweetReplyLikes = new TweetReplyLikes();
            tweetReplyLikes.LikedUserId = 1;
            tweetReplyLikes.ReplyId = 1;
            var result = await TweetCommentReplyLikeRepository.Add(tweetReplyLikes);

            var AddedTweet = await TweetCommentReplyLikeRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchTweetCommentReplyLikeFoundException>(async () => await TweetCommentReplyLikeRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such TweetComment Reply Like Found!", exception.Message);
        }

    }
}
