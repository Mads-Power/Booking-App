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
	public class SeatControllerTests
	{
        private readonly Mock<IRoomRepository> _mockRoomRepository;
        private readonly Mock<ISeatRepository> _mockSeatRepository;
        private readonly IMapper _mapper;
        private readonly SeatController _controller;

        public SeatControllerTests()
		{
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new SeatProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _mockRoomRepository = new Mock<IRoomRepository>();
            _mockSeatRepository = new Mock<ISeatRepository>();
            _controller = new SeatController(_mapper,
                _mockRoomRepository.Object, _mockSeatRepository.Object);
        }

        private static List<Seat> GetTestSeats()
        {
            var seat1 = new Seat()
            {
                Id = 1,
                Name = "Test Seat 1",
                RoomId = 1,
                Bookings = new List<Booking>()
            };
            var seat2 = new Seat()
            {
                Id = 2,
                Name = "Test Seat 2",
                RoomId = 2,
                Bookings = new List<Booking>()
        };
            return new List<Seat>() { seat1, seat2 };
        }

        [Fact]
        public async void GetAllSeats_WhenCalled_ReturnsListOfAllSeats()
        {
            // Arrange
            _mockSeatRepository.Setup(repo => repo.GetSeatsAsync()).ReturnsAsync(GetTestSeats());

            // Act
            var actionResult = await _controller.GetAllSeats();

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllSeats_WhenEmpty_ReturnsEmptyList()
        {
            // Arrange
            _mockSeatRepository.Setup(repo => repo.GetSeatsAsync()).ReturnsAsync(new List<Seat>());

            // Act
            var actionResult = await _controller.GetAllSeats();

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetSeat_WhenExists_ReturnsSeat()
        {
            // Arrange
            var seatId = 1;
            _mockSeatRepository.Setup(repo => repo.GetSeatAsync(seatId)).ReturnsAsync(GetTestSeats()[0]);

            // Act
            var actionResult = await _controller.GetSeat(seatId);

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Equal(seatId, result.Id);
            Assert.Equal("Test Seat 1", result.Name);
        }

        [Fact]
        public async void GetSeat_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var seatId = 3;
            _mockSeatRepository.Setup(repo => repo.GetSeatAsync(seatId)); // returns null by default

            // Act
            var actionResult = await _controller.GetSeat(seatId);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void PostSeat_WhenValidModel_ReturnsNewSeat()
        {
            // Arrange
            var newSeat = new SeatCreateDTO() { Id = 3, Name = "Test Seat 3", RoomId = 1 };
            _mockRoomRepository.Setup(repo => repo.RoomExists(newSeat.RoomId)).Returns(true);

            // Act
            var actionResult = await _controller.PostSeat(newSeat);

            // Assert
            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var result = (actionResult.Result as CreatedAtActionResult)?.Value as SeatReadDTO;
            Assert.NotNull(result);
            Assert.Equal(newSeat.Id, result.Id);
            Assert.Equal(newSeat.Name, result.Name);
        }

        [Fact]
        public async void PostSeat_WhenInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var newSeat = new SeatCreateDTO() { Id = 3, Name = "Test Seat 3", RoomId = 10 };
            _mockRoomRepository.Setup(repo => repo.RoomExists(newSeat.RoomId)).Returns(false);

            // Act
            var actionResult = await _controller.PostSeat(newSeat);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void PutSeat_WhenValidModel_ReturnsNoContent()
        {
            // Arrange
            var updateSeat = new SeatEditDTO() { Id = 1, Name = "Updated Seat 1", RoomId = 2 };
            _mockSeatRepository.Setup(repo => repo.GetSeatAsync(1)).ReturnsAsync(GetTestSeats()[0]);
            _mockRoomRepository.Setup(repo => repo.RoomExists(updateSeat.RoomId)).Returns(true);

            // Act
            var actionResult = await _controller.PutSeat(1, updateSeat);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void PutSeat_WhenInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var updateSeat = new SeatEditDTO() { Id = 1, Name = "Updated Seat 1", RoomId = 2 };
            _mockSeatRepository.Setup(repo => repo.GetSeatAsync(1)).ReturnsAsync(GetTestSeats()[0]);

            // Act
            var actionResult = await _controller.PutSeat(2, updateSeat);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void PutSeat_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var updateSeat = new SeatEditDTO() { Id = 10, Name = "Not Found Seat", RoomId = 1 };
            _mockSeatRepository.Setup(repo => repo.GetSeatAsync(1));
            _mockRoomRepository.Setup(repo => repo.RoomExists(updateSeat.RoomId)).Returns(true);

            // Act
            var actionResult = await _controller.PutSeat(10, updateSeat);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void DeleteSeat_WhenExists_ReturnsNoContent()
        {
            // Arrange
            var seatId = 2;
            _mockSeatRepository.Setup(repo => repo.SeatExists(seatId)).Returns(true);

            // Act
            var actionResult = await _controller.DeleteSeat(seatId);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void DeleteSeat_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var seatId = 3;
            _mockSeatRepository.Setup(repo => repo.SeatExists(seatId)).Returns(false);

            // Act
            var actionResult = await _controller.DeleteSeat(seatId);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}

