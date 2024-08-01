using BloggingApp.Contexts;
using BloggingApp.Exceptions.CommentExceptions;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Models;
using BloggingApp.Models.CommentDTOs;
using BloggingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAppTest.RepositoryTests
{
    public class CommentRepositoryTest
    {
        BloggingAppContext context;
        CommentRepository commentRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            commentRepository = new CommentRepository(context);
        }

        [Test]
        public async Task AddCommentSuccessTest()
        {
            //Arrange
            Comment comment = new Comment();
            comment.CommentContent ="comment";
            comment.CommentDateTime = DateTime.Now;
            comment.UserId = 1;
            comment.TweetId = 1;
            var result = await commentRepository.Add(comment);

            //Assert
            Assert.AreEqual(1, result.UserId);
        }


        [Test]
        public async Task DeleteCommentSuccessTest()
        {
            //Arrange
            Comment comment = new Comment();
            comment.CommentContent = "comment";
            comment.CommentDateTime = DateTime.Now;
            comment.UserId = 1;
            comment.TweetId = 1;
            var result = await commentRepository.Add(comment);

            var DeletedTweet = await commentRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.UserId);
        }

        [Test]
        public async Task TweetCommentFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchCommentFoundException>(async () => await commentRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such Comment Found!", exception.Message);
        }


        [Test]
        public async Task GetCommentByKeySuccessTest()
        {
            Comment comment = new Comment();
            comment.CommentContent = "comment";
            comment.CommentDateTime = DateTime.Now;
            comment.UserId = 1;
            comment.TweetId = 1;
            var result = await commentRepository.Add(comment);

            var AddedTweet = await commentRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.UserId);
        }

        [Test]
        public async Task GetCommentbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchCommentFoundException>(async () => await commentRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such Comment Found!", exception.Message);
        }


        [Test]
        public async Task GetAllCommentSuccessTest()
        {
            //Arrange
            var result = await commentRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllCommentFailTest()
        {
            //Arrange
            var result = await commentRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateCommentSuccessTest()
        {
            //Arrange
            Comment comment = new Comment();
            comment.CommentContent = "comment";
            comment.CommentDateTime = DateTime.Now;
            comment.UserId = 1;
            comment.TweetId = 1;
            var result = await commentRepository.Add(comment);

            var AddedTweet = await commentRepository.GetbyKey(result.Id);
            AddedTweet.TweetId = 111;

            var UpdatedResult = await commentRepository.Update(result);

            //Assert
            Assert.AreEqual(111, result.TweetId);
        }

        [Test]
        public async Task UpdateCommentFailTest()
        {
            //Arrange
            Comment comment = new Comment();
            comment.CommentContent = "comment";
            comment.CommentDateTime = DateTime.Now;
            comment.UserId = 1;
            comment.TweetId = 1;
            var result = await commentRepository.Add(comment);


            var AddedTweet = await commentRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchCommentFoundException>(async () => await commentRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such Comment Found!", exception.Message);
        }


    }
}
