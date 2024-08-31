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
    public class AppointmentControllerTests
    {
        private Mock<IAppointmentService> _mockAppointmentService;
        private AppointmentController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockAppointmentService = new Mock<IAppointmentService>();
            _controller = new AppointmentController(_mockAppointmentService.Object);
        }

        [Test]
        public async Task SetAppointment_ValidModel_ReturnsOk()
        {
            // Arrange
            var appointmentDto = new AppointmentDto { Id = Guid.NewGuid() };
            _mockAppointmentService.Setup(service => service.SetAppointment(It.IsAny<AppointmentDto>()))
                .ReturnsAsync(appointmentDto);

            // Act
            var result = await _controller.SetAppointment(appointmentDto);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(appointmentDto, okResult.Value);
        }

        [Test]
        public async Task SetAppointment_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Error", "Invalid model");

            // Act
            var result = await _controller.SetAppointment(new AppointmentDto());

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task SetAppointment_ThrowsException_ReturnsBadRequest()
        {
            // Arrange
            var appointmentDto = new AppointmentDto();
            _mockAppointmentService.Setup(service => service.SetAppointment(appointmentDto))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.SetAppointment(appointmentDto);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);

            var value = badRequestResult.Value;

            // Assert that the value is not null
            Assert.IsNotNull(value);

            // Deserialize the object to a known type
            var errorResponse = value.ToString();

            // Assert that the message matches the expected message
            Assert.AreEqual("{ message = Test exception }", errorResponse);
        }

        [Test]
        public async Task GetMyAppointments_ValidRequest_ReturnsOk()
        {
            // Arrange
            var appointmentListDto = new AppointmentListDto
            {
                Appointments = new List<AppointmentDto>
                {
                    new AppointmentDto { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid() }
                },
                TotalCounts = 1
            };

            _mockAppointmentService.Setup(service => service.GetMyAppointments(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(appointmentListDto);

            // Act
            var result = await _controller.GetMyAppointments(Guid.NewGuid(), 0, 10);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(appointmentListDto, okResult.Value);
        }

        [Test]
        public async Task GetMyAppointments_ThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            _mockAppointmentService.Setup(service => service.GetMyAppointments(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Error occurred"));

            // Act
            var result = await _controller.GetMyAppointments(Guid.NewGuid(), 0, 10);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("Internal server error: Error occurred", statusCodeResult.Value);
        }

        [Test]
        public async Task GetAllAppointments_ValidRequest_ReturnsOk()
        {
            // Arrange
            var appointmentListDto = new AppointmentListDto
            {
                Appointments = new List<AppointmentDto>
                {
                    new AppointmentDto { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid() }
                },
                TotalCounts = 1
            };

            _mockAppointmentService.Setup(service => service.GetAllAppointments(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync(appointmentListDto);

            // Act
            var result = await _controller.GetAllAppointments(1, 10, DateTime.Now);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(appointmentListDto, okResult.Value);
        }

        [Test]
        public async Task GetAllAppointments_ThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            _mockAppointmentService.Setup(service => service.GetAllAppointments(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new Exception("Error occurred"));

            // Act
            var result = await _controller.GetAllAppointments(1, 10, DateTime.Now);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("Internal server error: Error occurred", statusCodeResult.Value);
        }
    }
}
