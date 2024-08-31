using Agency.API.Controllers;
using Agency.Application.DTOs;
using Agency.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agency.Tests.Controllers
{
    [TestFixture]
    public class OffDayControllerTests
    {
        private Mock<IOffDayService> _mockOffDayService;
        private OffDayController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockOffDayService = new Mock<IOffDayService>();
            _controller = new OffDayController(_mockOffDayService.Object);
        }

        [Test]
        public async Task SetOffDay_ValidDate_ReturnsOk()
        {
            // Arrange
            var date = DateTime.Now;
            _mockOffDayService.Setup(service => service.SetOffDay(date))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.SetOffDay(date);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task SetOffDay_InvalidDate_ReturnsBadRequest()
        {
            // Arrange
            var date = DateTime.Now;
            _mockOffDayService.Setup(service => service.SetOffDay(date))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.SetOffDay(date);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task GetOffDays_ReturnsOffDays()
        {
            // Arrange
            var offDays = new List<OffDayDto>
            {
                new OffDayDto { Id = Guid.NewGuid(), Day = DateTime.Now },
                new OffDayDto { Id = Guid.NewGuid(), Day = DateTime.Now.AddDays(1) }
            };

            _mockOffDayService.Setup(service => service.GetOffDays())
                .ReturnsAsync(offDays);

            // Act
            var result = await _controller.GetOffDays();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedOffDays = okResult.Value as List<OffDayDto>;
            Assert.AreEqual(offDays.Count, returnedOffDays.Count);
        }

        [Test]
        public async Task RemoveOffDay_ValidDate_ReturnsOk()
        {
            // Arrange
            var date = DateTime.Now;
            _mockOffDayService.Setup(service => service.RemoveOffDay(date))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.RemoveOffDay(date);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task RemoveOffDay_InvalidDate_ReturnsNotFound()
        {
            // Arrange
            var date = DateTime.Now;
            _mockOffDayService.Setup(service => service.RemoveOffDay(date))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.RemoveOffDay(date);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task RemoveOffDay_ThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            var date = DateTime.Now;
            _mockOffDayService.Setup(service => service.RemoveOffDay(It.IsAny<DateTime>()))
                .ThrowsAsync(new Exception("Error occurred"));

            // Act
            var result = await _controller.RemoveOffDay(date);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("Internal server error: Error occurred", statusCodeResult.Value);
        }
    }
}
