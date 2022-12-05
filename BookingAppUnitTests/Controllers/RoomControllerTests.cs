using System;
using AutoMapper;
using BookingApp.Controllers;
using BookingApp.Models.Domain;
using BookingApp.Models.DTOs;
using BookingApp.Profiles;
using BookingApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookingAppUnitTests.Controllers
{
	public class RoomControllerTests
	{
        private readonly Mock<IRoomRepository> _mockRoomRepository;
        private readonly IMapper _mapper;
        private readonly RoomController _controller;

        public RoomControllerTests()
		{
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new RoomProfile());
                    mc.AddProfile(new SeatProfile());
                    mc.AddProfile(new BookingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            // mock date
            var fixedDate = new FixedDateTimeProvider(new DateTime(2021, 1, 1));

            _mockRoomRepository = new Mock<IRoomRepository>();
            _controller = new RoomController(_mapper,
                _mockRoomRepository.Object, fixedDate);
        }

        private static List<Room> GetTestRooms()
        {
            var room1 = new Room()
            {
                Id = 1,
                Name = "Test Room 1",
                Capacity = 10,
                OfficeId = 1
            };
            var room2 = new Room()
            {
                Id = 2,
                Name = "Test Room 2",
                Capacity = 5,
                OfficeId = 2
            };
            return new List<Room>() { room1, room2 };
        }

        [Fact]
        public async void GetAllRooms_WhenCalled_ReturnsListOfAllRooms()
        {
            // Arrange
            _mockRoomRepository.Setup(repo => repo.GetRoomsAsync()).ReturnsAsync(GetTestRooms());

            // Act
            var actionResult = await _controller.GetAllRooms();

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllRooms_WhenEmpty_ReturnsEmptyList()
        {
            // Arrange
            _mockRoomRepository.Setup(repo => repo.GetRoomsAsync()).ReturnsAsync(new List<Room>());

            // Act
            var actionResult = await _controller.GetAllRooms();

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetRoom_WhenExists_ReturnsRoom()
        {
            // Arrange
            var roomId = 1;
            _mockRoomRepository.Setup(repo => repo.GetRoomAsync(roomId)).ReturnsAsync(GetTestRooms()[0]);

            // Act
            var actionResult = await _controller.GetRoom(roomId);

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Equal(roomId, result.Id);
            Assert.Equal("Test Room 1", result.Name);
        }

        [Fact]
        public async void GetRoom_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var roomId = 3;
            _mockRoomRepository.Setup(repo => repo.GetRoomAsync(roomId)); // returns null by default

            // Act
            var actionResult = await _controller.GetRoom(roomId);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void PostRoom_WhenValidModel_ReturnsNewRoom()
        {
            // Arrange
            var newRoom = new RoomCreateDTO() { Id = 3, Name = "Test Room 3", Capacity = 3, OfficeId = 1 };

            // Act
            var actionResult = await _controller.PostRoom(newRoom);

            // Assert
            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var result = (actionResult.Result as CreatedAtActionResult)?.Value as RoomReadDTO;
            Assert.NotNull(result);
            Assert.Equal(newRoom.Id, result.Id);
            Assert.Equal(newRoom.Name, result.Name);
        }

        // TODO: add unit test when bad request validation is added to post
        //[Fact]
        //public async void PostRoom_WhenInvalidModel_ReturnsBadRequest()
        //{
        //}

        [Fact]
        public async void PutRoom_WhenValidModel_ReturnsNoContent()
        {
            // Arrange
            var updateRoom = new RoomEditDTO() { Id = 1, Name = "Updated Room 1", OfficeId = 2 };
            _mockRoomRepository.Setup(repo => repo.GetRoomAsync(1)).ReturnsAsync(GetTestRooms()[0]);

            // Act
            var actionResult = await _controller.PutRoom(1, updateRoom);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void PutRoom_WhenInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var updateRoom = new RoomEditDTO() { Id = 1, Name = "Updated Room 1", OfficeId = 2 };
            _mockRoomRepository.Setup(repo => repo.GetRoomAsync(1)).ReturnsAsync(GetTestRooms()[0]);

            // Act
            var actionResult = await _controller.PutRoom(2, updateRoom);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void PutRoom_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var updateRoom = new RoomEditDTO() { Id = 10, Name = "Not Found Room", OfficeId = 1 };
            _mockRoomRepository.Setup(repo => repo.GetRoomAsync(1));
            _mockRoomRepository.Setup(repo => repo.RoomExists(updateRoom.OfficeId)).Returns(true);

            // Act
            var actionResult = await _controller.PutRoom(10, updateRoom);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void DeleteRoom_WhenExists_ReturnsNoContent()
        {
            // Arrange
            var roomId = 2;
            _mockRoomRepository.Setup(repo => repo.RoomExists(roomId)).Returns(true);

            // Act
            var actionResult = await _controller.DeleteRoom(roomId);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void DeleteRoom_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var roomId = 3;
            _mockRoomRepository.Setup(repo => repo.RoomExists(roomId)).Returns(false);

            // Act
            var actionResult = await _controller.DeleteRoom(roomId);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void GetSeatsInRoom_WhenExists_ReturnsSeats()
        {
            // Arrange
            var roomId = 1;
            _mockRoomRepository.Setup(repo => repo.GetSeatsInRoom(roomId)).ReturnsAsync(new List<Seat>()
            {
                new Seat() {Id = 1, Name = "Test Seat 1", RoomId = 1},
                new Seat() {Id = 2, Name = "Test Seat 2", RoomId = 1}
            });

            // Act
            var actionResult = await _controller.GetSeatsInRoom(roomId);

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetSeatsInRoom_WhenNexists_ReturnsEmptyList()
        {
            // Arrange
            var roomId = 1;
            _mockRoomRepository.Setup(repo => repo.GetSeatsInRoom(roomId)).ReturnsAsync(new List<Seat>());

            // Act
            var actionResult = await _controller.GetSeatsInRoom(roomId);

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetBookingsInRoomByDate_WhenExists_ReturnBookings()
        {
            // Arrange
            var roomId = 1;
            var date = "01-01-2022";
            var mockDate = new FixedDateTimeProvider().Parse(date);
            _mockRoomRepository.Setup(repo => repo.GetBookingsInRoomByDate(roomId, mockDate)).ReturnsAsync(new List<Booking>()
            {
                new Booking() {Id = 1, UserId = 1, SeatId = 1},
                new Booking() {Id = 2, UserId = 2, SeatId = 2}
            });

            // Act
            var actionResult = await _controller.GetBookingsInRoomByDate(roomId, date);

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetBookingsInRoomByDate_WhenNexists_ReturnEmptyList()
        {
            // Arrange
            var roomId = 1;
            var date = "01-01-2022";
            var mockDate = new FixedDateTimeProvider().Parse(date);
            _mockRoomRepository.Setup(repo => repo.GetBookingsInRoomByDate(roomId, mockDate)).ReturnsAsync(new List<Booking>());

            // Act
            var actionResult = await _controller.GetBookingsInRoomByDate(roomId, date);

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}

