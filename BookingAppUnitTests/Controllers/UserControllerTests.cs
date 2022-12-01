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
            var fixedDate = new FixedDateTimeProvider(new DateTime(2021, 1, 1));

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
                Id = 1,
                Name = "Test User 1",
                OfficeId = 1
            };
            var user2 = new User()
            {
                Id = 2,
                Name = "Test User 2",
                OfficeId = 2
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
            var userId = 1;
            _mockUserRepository.Setup(repo => repo.GetUserAsync(userId)).ReturnsAsync(GetTestUsers()[0]);

            // Act
            var actionResult = await _controller.GetUser(userId);

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal("Test User 1", result.Name);
        }

        [Fact]
        public async void GetUser_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var userId = 3;
            _mockUserRepository.Setup(repo => repo.GetUserAsync(userId)); // returns null by default

            // Act
            var actionResult = await _controller.GetUser(userId);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void PostUser_WhenValidModel_ReturnsNewUser()
        {
            // Arrange
            var newUser = new UserCreateDTO() { Id = 3, Name = "Test User 3", OfficeId = 1 };

            // Act
            var actionResult = await _controller.PostUser(newUser);

            // Assert
            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var result = (actionResult.Result as CreatedAtActionResult)?.Value as UserReadDTO;
            Assert.NotNull(result);
            Assert.Equal(newUser.Id, result.Id);
            Assert.Equal(newUser.Name, result.Name);
        }

        // TODO: add unit test when bad request validation is added to post
        //[Fact]
        //public async void PostUser_WhenInvalidModel_ReturnsBadRequest()
        //{
        //}

        [Fact]
        public async void PutUser_WhenValidModel_ReturnsNoContent()
        {
            // Arrange
            var updateUser = new UserEditDTO() { Id = 1, Name = "Updated User 1", OfficeId = 2 };
            _mockUserRepository.Setup(repo => repo.GetUserAsync(1)).ReturnsAsync(GetTestUsers()[0]);

            // Act
            var actionResult = await _controller.PutUser(1, updateUser);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void PutUser_WhenInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var updateUser = new UserEditDTO() { Id = 1, Name = "Updated User 1", OfficeId = 2 };
            _mockUserRepository.Setup(repo => repo.GetUserAsync(1)).ReturnsAsync(GetTestUsers()[0]);

            // Act
            var actionResult = await _controller.PutUser(2, updateUser);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void PutUser_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var updateUser = new UserEditDTO() { Id = 10, Name = "Not Found User", OfficeId = 1 };
            _mockUserRepository.Setup(repo => repo.GetUserAsync(1));

            // Act
            var actionResult = await _controller.PutUser(10, updateUser);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void DeleteUser_WhenExists_ReturnsNoContent()
        {
            // Arrange
            var userId = 2;
            _mockUserRepository.Setup(repo => repo.UserExists(userId)).Returns(true);

            // Act
            var actionResult = await _controller.DeleteUser(userId);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void DeleteUser_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var userId = 3;
            _mockUserRepository.Setup(repo => repo.UserExists(userId)).Returns(false);

            // Act
            var actionResult = await _controller.DeleteUser(userId);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void UserBookSeat_WhenValidRequest_ReturnsNoContent()
        {
            // Arrange
            var userId = 1;
            var seatId = 1;
            var date = "01-01-2022";
            _mockUserRepository.Setup(repo => repo.GetUserAsync(userId)).ReturnsAsync(GetTestUsers()[0]);
            _mockSeatRepository.Setup(repo => repo.GetSeatAsync(seatId)).ReturnsAsync(new Seat() { Id = 1, Name = "01", RoomId = 1 });

            // Act
            var actionResult = await _controller.UserBookSeat(userId, seatId, date);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void UserBookSeat_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var userId = 1;
            var seatId = 1;
            var date = "01-01-2022";
            _mockUserRepository.Setup(repo => repo.GetUserAsync(userId));

            // Act
            var actionResult = await _controller.UserBookSeat(userId, seatId, date);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void UserBookSeat_WhenInvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var userId = 1;
            var seatId = 1;
            var date = "";

            // Act
            var actionResult = await _controller.UserBookSeat(userId, seatId, date);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void UserUnbookSeat_WhenValidRequest_ReturnsNoContent()
        {
            // Arrange
            var userId = 1;
            var date = "01-01-2022";
            var mockDate = new FixedDateTimeProvider().Parse(date);
            
            _mockUserRepository.Setup(repo => repo.GetUserAsync(userId)).ReturnsAsync(GetTestUsers()[0]);
            _mockBookingRepository.Setup(repo => repo.GetBookingByDateAndUser(mockDate, userId))
                .Returns(new Booking() { Id = 1, Date = mockDate, UserId = 1, SeatId = 1});
            // Struggeling to mock this bookingservice

            // Act
            var actionResult = await _controller.UserUnbookSeat(userId, date);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void UserUnbookSeat_WhenNexists_ReturnsBadRequest()
        {
            // Arrange
            var userId = 3;
            var date = "01-01-2022";
            var mockDate = new FixedDateTimeProvider().Parse(date);

            _mockUserRepository.Setup(repo => repo.GetUserAsync(userId));
            _mockBookingRepository.Setup(repo => repo.GetBookingByDateAndUser(mockDate, userId));

            // Act
            var actionResult = await _controller.UserUnbookSeat(userId, date);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void UserUnbookSeat_WhenInvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var userId = 1;
            var date = "";

            // Act
            var actionResult = await _controller.UserUnbookSeat(userId, date);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

    }
}

