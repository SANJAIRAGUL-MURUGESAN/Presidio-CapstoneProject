using BloggingApp.Contexts;
using BloggingApp.Exceptions.FollowExceptions;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Models;
using BloggingApp.Models.FollowDTOs;
using BloggingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAppTest.RepositoryTests
{
    public class FollowRepositoryTest
    {
        BloggingAppContext context;
        FollowRepository followRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            followRepository = new FollowRepository(context);
        }

        [Test]
        public async Task AddFollowSuccessTest()
        {
            //Arrange

            Follow follow = new Follow();
            follow.FollowerId = 1;
            follow.UserId = 1;
            var result = await followRepository.Add(follow);

            //Assert
            Assert.AreEqual(1, result.UserId);
        }

        [Test]
        public async Task DeleteFollowSuccessTest()
        {
            //Arrange
            Follow follow = new Follow();
            follow.FollowerId = 1;
            follow.UserId = 1;
            var result = await followRepository.Add(follow); 

            var DeletedTweet = await followRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual(1, DeletedTweet.UserId);
        }


        [Test]
        public async Task FollowDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchFollowerFoundException>(async () => await followRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such Follower Found!", exception.Message);
        }

        [Test]
        public async Task GetFollowerByKeySuccessTest()
        {
            //Arrange
            Follow follow = new Follow();
            follow.FollowerId = 1;
            follow.UserId = 1;
            var result = await followRepository.Add(follow); 

            var AddedTweet = await followRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual(1, AddedTweet.UserId);
        }
        [Test]
        public async Task GetFollowerbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchFollowerFoundException>(async () => await followRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such Follower Found!", exception.Message);
        }

        [Test]
        public async Task GetAllFollowerssSuccessTest()
        {
            //Arrange
            var result = await followRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllFollowersFailTest()
        {
            //Arrange
            var result = await followRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task UpdateFollowSuccessTest()
        {
            //Arrange
            Follow follow = new Follow();
            follow.FollowerId = 1;
            follow.UserId = 1;
            var result = await followRepository.Add(follow);

            var AddedTweet = await followRepository.GetbyKey(result.Id);

            AddedTweet.UserId = 12;

            var UpdatedResult = await followRepository.Update(result);

            //Assert
            Assert.AreEqual(12, result.UserId);
        }

        [Test]
        public async Task UpdateFollowerFailTest()
        {
            //Arrange
            Follow follow = new Follow();
            follow.FollowerId = 1;
            follow.UserId = 1;
            var result = await followRepository.Add(follow);

            var AddedTweet = await followRepository.GetbyKey(result.Id);
            AddedTweet.Id = 123456789;

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchFollowerFoundException>(async () => await followRepository.Update(AddedTweet));

            //Assert
            Assert.AreEqual("No Such Follower Found!", exception.Message);
        }
    }
}
