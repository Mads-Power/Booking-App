using System;
using BookingApp.Helpers;
using BookingApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookingAppUnitTests
{
	public class ValidationResultTests
	{
		//private readonly ValidationResult _validationResult;

		public ValidationResultTests()
		{
			//_validationResult
		}

		[Fact]
		public void ValidateDateString_WhenIllegalLength_ReturnFalse()
		{
			// Arrange
			var dateString = new String('X', 100);

			// Act
			var validation = ValidationResult.ValidateDateString(dateString);

			// Assert
			Assert.False(validation.Result);
        }

		[Fact]
        public void ValidateDateString_WhenIncorrectFormat_ReturnFalse()
        {
            // Arrange
            var dateString = "2020-$01-01##Z:00:00+";

            // Act
            var validation = ValidationResult.ValidateDateString(dateString);

            // Assert
            Assert.False(validation.Result);
        }

        [Fact]
        public void ValidateDateString_WhenValid_ReturnTrue()
        {
            // Arrange
            var dateString = "2020-01-01T13:45:30Z";

            // Act
            var validation = ValidationResult.ValidateDateString(dateString);

            // Assert
            Assert.True(validation.Result);
        }
    }
}

