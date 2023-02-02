using System;
using AutoMapper;
using BookingApp.Controllers;
using BookingApp.Helpers;
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
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ISeatRepository> _mockSeatRepository;
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
            // mock date
            var fixedDate = new FixedDateTimeProvider(new DateTime(2022, 12, 12).ToUniversalTime());

            _mockBookingRepository = new Mock<IBookingRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockSeatRepository = new Mock<ISeatRepository>();
            _controller = new BookingController(_mapper,
                _mockBookingRepository.Object, _mockUserRepository.Object, _mockSeatRepository.Object, fixedDate);
        }

        private static List<Booking> GetTestBookings()
        {
            var booking1 = new Booking()
            {
                Id = 1,
                UserId = "1",
                SeatId = 1,
                Email = "one@test.com",
                Date = new DateTime(2022, 12, 12)
            };
            var booking2 = new Booking()
            {
                Id = 2,
                UserId = "2",
                SeatId = 2,
                Email ="two@test.com",
                Date = new DateTime(2021, 12, 12)
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
            var newBooking = new BookingCreateDTO() { SeatId = 3, Email = "three@test.com", Date = new DateTime(2022, 12, 13) };
            _mockUserRepository.Setup(repo => repo.UserExists(newBooking.Email)).Returns(true);
            _mockSeatRepository.Setup(repo => repo.SeatExists(newBooking.SeatId)).Returns(true);
            _mockBookingRepository.Setup(repo => repo.GetBookingByDateAndUser(newBooking.Date, newBooking.Email));
            _mockBookingRepository.Setup(repo => repo.GetBookingByDateAndSeat(newBooking.Date, newBooking.SeatId));

            // Act
            var actionResult = await _controller.PostBooking(newBooking);

            // Assert
            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var result = (actionResult.Result as CreatedAtActionResult)?.Value as BookingReadDTO;
            Assert.NotNull(result);
            Assert.Equal(newBooking.Email, result.Email);
        }

        [Fact]
        public async void PostBooking_WhenInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var newBooking = new BookingCreateDTO() { SeatId = 3, Email = "three@test.com", Date = new DateTime(2022, 12, 1) };

            // Act
            var actionResult = await _controller.PostBooking(newBooking);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void PutBooking_WhenValidModel_ReturnsNoContent()
        {
            // Arrange
            var updateBooking = new BookingEditDTO() {Id = 1, SeatId = 3, Email = "three@test.com" };
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
            var updateBooking = new BookingEditDTO() { Id = 1, SeatId = 1, Email = "two@test.com", Date = new DateTime(2023, 01, 01) };
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
            var updateBooking = new BookingEditDTO() { Id = 10, SeatId = 10, Email = "ten@test.com", Date = new DateTime(2021, 01, 01) };
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

        [Fact]
        public async void BookSeat_WhenValidRequest_ReturnsNewBooking()
        {
            // Arrange
            var newBooking = new BookingBookDTO() { SeatId = 1, Email = "one@test.com", Date = "2023-01-01T23:00:00Z" };
            var dateTime = DateTime.Parse(newBooking.Date);
            var user = new User()
            {
                Id = "1",
                Name = "Test User 1",
                Email = "one@test.com",
                Bookings = new List<Booking>()
            };
            var seat = new Seat() { Id = 1, Name = "01", RoomId = 1 };
            var mockResultBooking = new Booking() { Id = 1, Email = "one@test.com", SeatId = 1, Date = dateTime };

            _mockUserRepository.Setup(repo => repo.GetUserAsync(newBooking.Email)).ReturnsAsync(user);
            _mockSeatRepository.Setup(repo => repo.GetSeatAsync(newBooking.SeatId)).ReturnsAsync(seat);
            _mockBookingRepository.Setup(repo => repo.BookSeat(user, seat, dateTime)).Returns(Task.FromResult<Booking>(mockResultBooking));

            // Act
            var actionResult = await _controller.BookSeat(newBooking);

            // Assert
            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var result = (actionResult.Result as CreatedAtActionResult)?.Value as BookingReadDTO;
            Assert.NotNull(result);
            Assert.Equal(newBooking.Email, result.Email);
        }

        [Fact]
        public async void BookSeat_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var newBooking = new BookingBookDTO() { SeatId = 1, Email = "one@test.com", Date = "2023-01-01T23:00:00Z" };
            _mockUserRepository.Setup(repo => repo.GetUserAsync(newBooking.Email));
            _mockSeatRepository.Setup(repo => repo.GetSeatAsync(newBooking.SeatId));

            // Act
            var actionResult = await _controller.BookSeat(newBooking);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void BookSeat_WhenInvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var newBooking = new BookingBookDTO() { SeatId = 1, Email = "one@test.com", Date = "" };

            // Act
            var actionResult = await _controller.BookSeat(newBooking);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void UnbookSeat_WhenValidRequest_ReturnsNoContent()
        {
            // Arrange
            var newUnbooking = new BookingUnbookDTO() { Email = "one@test.com", Date = "2022-01-01T23:00:00Z" };
            var mockDate = new FixedDateTimeProvider().Parse(newUnbooking.Date);

            _mockUserRepository.Setup(repo => repo.GetUserAsync(newUnbooking.Email)).ReturnsAsync(new User()
            {
                Id = "1",
                Name = "Test User 1",
                Bookings = new List<Booking>()
            });
            _mockBookingRepository.Setup(repo => repo.GetBookingByDateAndUser(mockDate, newUnbooking.Email))
                .Returns(new Booking() { Id = 1, Date = mockDate, UserId = "1", SeatId = 1 });
            // Struggeling to mock this bookingservice

            // Act
            var actionResult = await _controller.UnbookSeat(newUnbooking);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void UnbookSeat_WhenNexists_ReturnsBadRequest()
        {
            // Arrange
            var newUnbooking = new BookingUnbookDTO() { Email = "three@test.com", Date = "2022-01-01T00:00:00Z" };
            var mockDate = new FixedDateTimeProvider().Parse(newUnbooking.Date);

            _mockUserRepository.Setup(repo => repo.GetUserAsync(newUnbooking.Email));
            _mockBookingRepository.Setup(repo => repo.GetBookingByDateAndUser(mockDate, newUnbooking.Email));

            // Act
            var actionResult = await _controller.UnbookSeat(newUnbooking);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void UnbookSeat_WhenInvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var newUnbooking = new BookingUnbookDTO() { Email = "one@test.com", Date = "" };

            // Act
            var actionResult = await _controller.UnbookSeat(newUnbooking);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }
    }
}

