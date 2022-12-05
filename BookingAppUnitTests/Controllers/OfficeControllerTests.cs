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
	public class OfficeControllerTests
	{
        private readonly Mock<IOfficeRepository> _mockOfficeRepository;
        private readonly IMapper _mapper;
        private readonly OfficeController _controller;

        public OfficeControllerTests()
		{
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new OfficeProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _mockOfficeRepository = new Mock<IOfficeRepository>();
            _controller = new OfficeController(_mapper,
                _mockOfficeRepository.Object);
        }

        private static List<Office> GetTestOffices()
        {
            var office1 = new Office()
            {
                Id = 1,
                Name = "Test Office 1",
                Capacity = 1
            };
            var office2 = new Office()
            {
                Id = 2,
                Name = "Test Office 2",
                Capacity = 5
            };
            return new List<Office>() { office1, office2 };
        }

        [Fact]
        public async void GetAllOffices_WhenCalled_ReturnsListOfAllOffices()
        {
            // Arrange
            _mockOfficeRepository.Setup(repo => repo.GetOfficesAsync()).ReturnsAsync(GetTestOffices());

            // Act
            var actionResult = await _controller.GetAllOffices();

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllOffices_WhenEmpty_ReturnsEmptyList()
        {
            // Arrange
            _mockOfficeRepository.Setup(repo => repo.GetOfficesAsync()).ReturnsAsync(new List<Office>());

            // Act
            var actionResult = await _controller.GetAllOffices();

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetOffice_WhenExists_ReturnsOffice()
        {
            // Arrange
            var officeId = 1;
            _mockOfficeRepository.Setup(repo => repo.GetOfficeAsync(officeId)).ReturnsAsync(GetTestOffices()[0]);

            // Act
            var actionResult = await _controller.GetOffice(officeId);

            // Assert
            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Equal(officeId, result.Id);
            Assert.Equal("Test Office 1", result.Name);
        }

        [Fact]
        public async void GetOffice_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var officeId = 3;
            _mockOfficeRepository.Setup(repo => repo.GetOfficeAsync(officeId)); // returns null by default

            // Act
            var actionResult = await _controller.GetOffice(officeId);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async void PostOffice_WhenValidModel_ReturnsNewOffice()
        {
            // Arrange
            var newOffice = new OfficeCreateDTO() { Id = 3, Name = "Test Office 3", Capacity = 3 };

            // Act
            var actionResult = await _controller.PostOffice(newOffice);

            // Assert
            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var result = (actionResult.Result as CreatedAtActionResult)?.Value as OfficeReadDTO;
            Assert.NotNull(result);
            Assert.Equal(newOffice.Id, result.Id);
            Assert.Equal(newOffice.Name, result.Name);
        }

        // TODO: add unit test when bad request validation is added to post
        //[Fact]
        //public async void PostOffice_WhenInvalidModel_ReturnsBadRequest()
        //{
        //}

        [Fact]
        public async void PutOffice_WhenValidModel_ReturnsNoContent()
        {
            // Arrange
            var updateOffice = new OfficeEditDTO() { Id = 1, Name = "Updated Office 1"};
            _mockOfficeRepository.Setup(repo => repo.GetOfficeAsync(1)).ReturnsAsync(GetTestOffices()[0]);

            // Act
            var actionResult = await _controller.PutOffice(1, updateOffice);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void PutOffice_WhenInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var updateOffice = new OfficeEditDTO() { Id = 1, Name = "Updated Office 1"};
            _mockOfficeRepository.Setup(repo => repo.GetOfficeAsync(1)).ReturnsAsync(GetTestOffices()[0]);

            // Act
            var actionResult = await _controller.PutOffice(2, updateOffice);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void PutOffice_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var updateOffice = new OfficeEditDTO() { Id = 10, Name = "Not Found Office"};
            _mockOfficeRepository.Setup(repo => repo.GetOfficeAsync(1));
            _mockOfficeRepository.Setup(repo => repo.OfficeExists(updateOffice.Id)).Returns(true);

            // Act
            var actionResult = await _controller.PutOffice(10, updateOffice);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void DeleteOffice_WhenExists_ReturnsNoContent()
        {
            // Arrange
            var officeId = 2;
            _mockOfficeRepository.Setup(repo => repo.OfficeExists(officeId)).Returns(true);

            // Act
            var actionResult = await _controller.DeleteOffice(officeId);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void DeleteOffice_WhenNexists_ReturnsNotFound()
        {
            // Arrange
            var officeId = 3;
            _mockOfficeRepository.Setup(repo => repo.OfficeExists(officeId)).Returns(false);

            // Act
            var actionResult = await _controller.DeleteOffice(officeId);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}

