using System;
using AutoMapper;
using BookingApp.Controllers;
using BookingApp.Models.Domain;
using BookingApp.Models.DTOs;
using BookingApp.Profiles;
using BookingApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookingAppUnitTests.Controllers
{
	public class BookingControllerTests
	{
        private readonly Mock<IBookingRepository> _mockBookingRepository;
        private readonly IMapper _mapper;
        private readonly BookingController _controller;

        public BookingControllerTests()
		{
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new BookingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _mockBookingRepository = new Mock<IBookingRepository>();
            _controller = new BookingController(_mapper,
                _mockBookingRepository.Object);
        }

        private static List<Booking> GetTestBookings()
        {
            var booking1 = new Booking()
            {
                Id = 1,
                UserId = 1,
                SeatId = 1,
                Date = new DateTime(2021, 01, 01)
            };
            var booking2 = new Booking()
            {
                Id = 2,
                UserId = 2,
                SeatId = 2,
                Date = new DateTime(2021, 01, 01)
            };
            return new List<Booking>() { booking1, booking2 };
        }

        [Fact]
        public async void GetAllBookings_WhenCalled_ReturnsListOfAllBookings()
        {
            // Arrange
            _mockBookingRepository.Setup(repo => repo.GetBookingsAsync()).ReturnsAsync(GetTestBookings());

            // Act
            var actionResult = await _controller.GetAllBookings();

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllBookings_WhenEmpty_ReturnsEmptyList()
        {
            // Arrange
            _mockBookingRepository.Setup(repo => repo.GetBookingsAsync()).ReturnsAsync(new List<Booking>());

            // Act
            var actionResult = await _controller.GetAllBookings();

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetBooking_WhenExists_ReturnsBooking()
        {
            // Arrange
            var bookingId = 1;
            _mockBookingRepository.Setup(repo => repo.GetBookingAsync(bookingId)).ReturnsAsync(GetTestBookings()[0]);

            // Act
            var actionResult = await _controller.GetBooking(bookingId);

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Equal(bookingId, result.Id);
        }

        [Fact]
        public async void GetBooking_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var bookingId = 3;
            _mockBookingRepository.Setup(repo => repo.GetBookingAsync(bookingId)); // returns null by default

            // Act
            var actionResult = await _controller.GetBooking(bookingId);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void PostBooking_WhenValidModel_ReturnsNewBooking()
        {
            // Arrange
            var newBooking = new BookingCreateDTO() { SeatId = 3, UserId = 3 };

            // Act
            var actionResult = await _controller.PostBooking(newBooking);

            // Assert
            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var result = (actionResult.Result as CreatedAtActionResult)?.Value as BookingReadDTO;
            Assert.NotNull(result);
            Assert.Equal(newBooking.UserId, result.UserId);
        }

        // TODO: add unit test when bad request validation is added to post
        //[Fact]
        //public async void PostBooking_WhenInvalidModel_ReturnsBadRequest()
        //{
        //}

        [Fact]
        public async void PutBooking_WhenValidModel_ReturnsNoContent()
        {
            // Arrange
            var updateBooking = new BookingEditDTO() {Id = 1, SeatId = 3, UserId = 3 };
            _mockBookingRepository.Setup(repo => repo.GetBookingAsync(1)).ReturnsAsync(GetTestBookings()[0]);

            // Act
            var actionResult = await _controller.PutBooking(1, updateBooking);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void PutBooking_WhenInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var updateBooking = new BookingEditDTO() { Id = 1, SeatId = 1, UserId = 2, Date = new DateTime(2023, 01, 01) };
            _mockBookingRepository.Setup(repo => repo.GetBookingAsync(1)).ReturnsAsync(GetTestBookings()[0]);

            // Act
            var actionResult = await _controller.PutBooking(2, updateBooking);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void PutBooking_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var updateBooking = new BookingEditDTO() { Id = 10, SeatId = 10, UserId = 10, Date = new DateTime(2021, 01, 01) };
            _mockBookingRepository.Setup(repo => repo.GetBookingAsync(1));
            _mockBookingRepository.Setup(repo => repo.BookingExists(updateBooking.Id)).Returns(true);

            // Act
            var actionResult = await _controller.PutBooking(10, updateBooking);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void DeleteBooking_WhenExists_ReturnsNoContent()
        {
            // Arrange
            var bookingId = 2;
            _mockBookingRepository.Setup(repo => repo.BookingExists(bookingId)).Returns(true);

            // Act
            var actionResult = await _controller.DeleteBooking(bookingId);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void DeleteBooking_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var bookingId = 3;
            _mockBookingRepository.Setup(repo => repo.BookingExists(bookingId)).Returns(false);

            // Act
            var actionResult = await _controller.DeleteBooking(bookingId);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}

