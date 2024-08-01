using BloggingApp.Contexts;
using BloggingApp.Exceptions.UserExceptions;
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
    public class UserRepositoryTest
    {
        BloggingAppContext context;
        UserRepository userRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("BlogginAppDummyDB");
            context = new BloggingAppContext(optionsBuilder.Options);
            userRepository = new UserRepository(context);
        }

        [Test]
        public async Task UserRegisterSuccessTest()
        {
            //Arrange
            User user = new User();
            user.UserName = "Sanjai Ragul M";
            user.UserId = "sanjai25";
            user.UserEmail = "sanjai@gmail.com";
            user.UserPassword = "asd";
            user.UserGender = "Male";
            user.UserEmail = "sanjai@gmail.com";
            user.UserMobile = "1234567890";
            user.Location = "cbe";
            user.IsPremiumHolder = "No";
            user.DateOfBirth = "dob";
            user.Age = 22;
            user.BioDescription = "Engineer";
            user.UserProfileImgLink = "asdddasa";
            var result = await userRepository.Add(user);

            //Assert
            Assert.AreEqual("Sanjai Ragul M", result.UserName);
        }

        [Test]
        public async Task UserDeleteSuccessTest()
        {
            //Arrange
            User user = new User();
            user.UserName = "Sanjai Ragul M";
            user.UserId = "sanjai25";
            user.UserEmail = "sanjai@gmail.com";
            user.UserPassword = "asd";
            user.UserGender = "Male";
            user.UserEmail = "sanjai@gmail.com";
            user.UserMobile = "1234567890";
            user.Location = "cbe";
            user.IsPremiumHolder = "No";
            user.DateOfBirth = "dob";
            user.Age = 22;
            user.BioDescription = "Engineer";
            user.UserProfileImgLink = "asdddasa";
            var result = await userRepository.Add(user);

            var DeletedUser = await userRepository.Delete(result.Id);
            //Assert
            Assert.AreEqual("Sanjai Ragul M", DeletedUser.UserName);
        }

        [Test]
        public async Task UserDeleteFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchUserFoundException>(async () => await userRepository.Delete(13456));
            //Assert
            Assert.AreEqual("No Such User Found!", exception.Message);
        }

        [Test]
        public async Task GetUserByKeySuccessTest()
        {
            //Arrange
            User user = new User();
            user.UserName = "Sanjai Ragul M";
            user.UserId = "sanjai25";
            user.UserEmail = "sanjai@gmail.com";
            user.UserPassword = "asd";
            user.UserGender = "Male";
            user.UserEmail = "sanjai@gmail.com";
            user.UserMobile = "1234567890";
            user.Location = "cbe";
            user.IsPremiumHolder = "No";
            user.DateOfBirth = "dob";
            user.Age = 22;
            user.BioDescription = "Engineer";
            user.UserProfileImgLink = "asdddasa";
            var result = await userRepository.Add(user);
            var AddedUser = await userRepository.GetbyKey(result.Id);

            //Assert
            Assert.AreEqual("Sanjai Ragul M", AddedUser.UserName);
        }

        [Test]
        public async Task GetUserbyKeyFailTest()
        {
            ///Action
            var exception = Assert.ThrowsAsync<NoSuchUserFoundException>(async () => await userRepository.GetbyKey(13456));
            //Assert
            Assert.AreEqual("No Such User Found!", exception.Message);
        }

        [Test]
        public async Task GetAllUsersSuccessTest()
        {
            //Arrange
            var result = await userRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllUsersFailTest()
        {
            //Arrange
            var result = await userRepository.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateUserSuccessTest()
        {
            User user = new User();
            user.UserName = "Sanjai Ragul M";
            user.UserId = "sanjai25";
            user.UserEmail = "sanjai@gmail.com";
            user.UserPassword = "asd";
            user.UserGender = "Male";
            user.UserEmail = "sanjai@gmail.com";
            user.UserMobile = "1234567890";
            user.Location = "cbe";
            user.IsPremiumHolder = "No";
            user.DateOfBirth = "dob";
            user.Age = 22;
            user.BioDescription = "Engineer";
            user.UserProfileImgLink = "asdddasa";
            var result = await userRepository.Add(user);
            result.UserName = "SanjaiRagul";
            //Arrange
            var UpdatedResult = await userRepository.Update(result);

            //Assert
            Assert.AreEqual("SanjaiRagul", result.UserName);
        }

        [Test]
        public async Task UpdateUserFailTest()
        {
            User user = new User();
            user.UserName = "Sanjai Ragul M";
            user.UserId = "sanjai25";
            user.UserEmail = "sanjai@gmail.com";
            user.UserPassword = "asd";
            user.UserGender = "Male";
            user.UserEmail = "sanjai@gmail.com";
            user.UserMobile = "1234567890";
            user.Location = "cbe";
            user.IsPremiumHolder = "No";
            user.DateOfBirth = "dob";
            user.Age = 22;
            user.BioDescription = "Engineer";
            user.UserProfileImgLink = "asdddasa";
            var result = await userRepository.Add(user);
            result.UserName = "SanjaiRagul";
            //Arrange

            result.Id = 456;
            //Action
            var exception = Assert.ThrowsAsync<NoSuchUserFoundException>(async () => await userRepository.Update(result));

            //Assert
            Assert.AreEqual("No Such User Found!", exception.Message);
        }
    }

}
