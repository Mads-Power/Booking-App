using System;
using AutoMapper;
using BookingApp.Controllers;
using BookingApp.Repositories;
using BookingApp.Profiles;
using BookingApp.Models.Domain;
using BookingApp.Models.DTOs;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BookingApp.Helpers;

namespace BookingAppUnitTests.Controllers
{
	public class UserControllerTests
	{
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ISeatRepository> _mockSeatRepository;
        private readonly Mock<IBookingRepository> _mockBookingRepository;
        private readonly IMapper _mapper;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new UserProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            // mock date
            var fixedDate = new FixedDateTimeProvider(new DateTime(2021, 1, 1).ToUniversalTime());

            _mockUserRepository = new Mock<IUserRepository>();
            _mockSeatRepository = new Mock<ISeatRepository>();
            _mockBookingRepository = new Mock<IBookingRepository>();
            _controller = new UserController(_mapper,
                _mockUserRepository.Object, _mockSeatRepository.Object,
                _mockBookingRepository.Object, fixedDate);
        }

        private static List<User> GetTestUsers()
        {
            var user1 = new User()
            {
                Id = "1",
                Name = "Test User 1",
                Email = "one@test.com",
                Bookings = new List<Booking>()
            };
            var user2 = new User()
            {
                Id = "2",
                Name = "Test User 2",
                Email = "two@test.com",
                Bookings = new List<Booking>()
            };
            return new List<User>() { user1, user2 };
        }

        [Fact]
        public async void GetAllUsers_WhenCalled_ReturnsListOfAllUsers()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetUsersAsync()).ReturnsAsync(GetTestUsers());

            // Act
            var actionResult = await _controller.GetAllUsers();

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllUsers_WhenEmpty_ReturnsEmptyList()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetUsersAsync()).ReturnsAsync(new List<User>());

            // Act
            var actionResult = await _controller.GetAllUsers();

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetUser_WhenExists_ReturnsUser()
        {
            // Arrange
            var userEmail = "one@test.com";
            _mockUserRepository.Setup(repo => repo.GetUserAsync(userEmail)).ReturnsAsync(GetTestUsers()[0]);

            // Act
            var actionResult = await _controller.GetUser(userEmail);

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Equal(userEmail, result.Email);
            Assert.Equal("Test User 1", result.Name);
        }

        [Fact]
        public async void GetUser_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var userEmail = "three@test.com";
            _mockUserRepository.Setup(repo => repo.GetUserAsync(userEmail)); // returns null by default

            // Act
            var actionResult = await _controller.GetUser(userEmail);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void PostUser_WhenValidModel_ReturnsNewUser()
        {
            // Arrange
            var newUser = new UserCreateDTO() { Email = "three@test.com", Name = "Test User 3" };
            _mockUserRepository.Setup(repo => repo.UserExists(newUser.Email)).Returns(false);

            // Act
            var actionResult = await _controller.PostUser(newUser);

            // Assert
            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var result = (actionResult.Result as CreatedAtActionResult)?.Value as UserReadDTO;
            Assert.NotNull(result);
            Assert.Equal(newUser.Email, result.Email);
            Assert.Equal(newUser.Name, result.Name);
        }

        [Fact]
        public async void PostUser_WhenInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var newUser = new UserCreateDTO() { Email = "three@test.com" };
            _mockUserRepository.Setup(repo => repo.UserExists(newUser.Email)).Returns(false);

            // Act
            var actionResult = await _controller.PostUser(newUser);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void PutUser_WhenValidModel_ReturnsNoContent()
        {
            // Arrange
            var updateUser = new UserEditDTO() { Email = "one@test.com", Name = "Updated User 1" };
            _mockUserRepository.Setup(repo => repo.GetUserAsync("one@test.com")).ReturnsAsync(GetTestUsers()[0]);

            // Act
            var actionResult = await _controller.PutUser("one@test.com", updateUser);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void PutUser_WhenInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var updateUser = new UserEditDTO() { Email = "one@test.com", Name = "Updated User 1" };
            _mockUserRepository.Setup(repo => repo.GetUserAsync("one@test.com")).ReturnsAsync(GetTestUsers()[0]);

            // Act
            var actionResult = await _controller.PutUser("two@test.com", updateUser);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void PutUser_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var updateUser = new UserEditDTO() { Email = "ten@test.com", Name = "Not Found User"};
            _mockUserRepository.Setup(repo => repo.GetUserAsync("one@test.com"));

            // Act
            var actionResult = await _controller.PutUser("ten@test.com", updateUser);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void DeleteUser_WhenExists_ReturnsNoContent()
        {
            // Arrange
            var userEmail = "two@test.com";
            _mockUserRepository.Setup(repo => repo.UserExists(userEmail)).Returns(true);

            // Act
            var actionResult = await _controller.DeleteUser(userEmail);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void DeleteUser_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var userEmail = "three@test.com";
            _mockUserRepository.Setup(repo => repo.UserExists(userEmail)).Returns(false);

            // Act
            var actionResult = await _controller.DeleteUser(userEmail);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}

