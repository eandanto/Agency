using Agency.API.Controllers;
using Agency.Application.DTOs;
using Agency.Application.Interfaces;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agency.Tests.Controllers
{
    [TestFixture]
    public class ConfigurationControllerTests
    {
        private Mock<IConfigurationService> _mockConfigurationService;
        private ConfigurationController _controller;

        [SetUp]
        public void Setup()
        {
            _mockConfigurationService = new Mock<IConfigurationService>();
            _controller = new ConfigurationController(_mockConfigurationService.Object);
        }

        [Test]
        public async Task GetConfigurations_ReturnsOkResultWithConfigurations()
        {
            // Arrange
            var configurations = new List<ConfigurationDto>
            {
                new ConfigurationDto { Id = Guid.NewGuid(), Value = "5" },
                new ConfigurationDto { Id = Guid.NewGuid(), Value = "10" }
            };
            _mockConfigurationService.Setup(service => service.GetAllConfigurations()).ReturnsAsync(configurations);

            // Act
            var result = await _controller.GetConfigurations();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnValue = okResult.Value as List<ConfigurationDto>;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(2, returnValue.Count);
        }

        [Test]
        public async Task GetConfigurations_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _mockConfigurationService.Setup(service => service.GetAllConfigurations())
                                     .ThrowsAsync(new System.Exception("Something went wrong"));

            // Act
            var result = await _controller.GetConfigurations();

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            var objectResult = result.Result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.IsTrue(objectResult.Value.ToString().Contains("Something went wrong"));
        }

        [Test]
        public async Task UpdateConfiguration_ValidConfigurations_ReturnsOkResult()
        {
            // Arrange
            var configurations = new List<ConfigurationDto>
            {
                new ConfigurationDto { Id = Guid.NewGuid(), Value = "5" },
                new ConfigurationDto { Id = Guid.NewGuid(), Value = "10" }
            };

            _mockConfigurationService.Setup(service => service.UpdateConfiguration(It.IsAny<ConfigurationDto>()))
                                     .Returns(Task.FromResult(true));

            // Act
            var result = await _controller.UpdateConfiguration(configurations);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task UpdateConfiguration_NullConfigurations_ReturnsInternalServerError()
        {
            // Act
            var result = await _controller.UpdateConfiguration(null);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.IsTrue(objectResult.Value.ToString().Contains("Nothing to update"));
        }

        [Test]
        public async Task UpdateConfiguration_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var configurations = new List<ConfigurationDto>
            {
                new ConfigurationDto { Id = Guid.NewGuid(), Value = "5" }
            };

            _mockConfigurationService.Setup(service => service.UpdateConfiguration(It.IsAny<ConfigurationDto>()))
                                     .ThrowsAsync(new System.Exception("Something went wrong"));

            // Act
            var result = await _controller.UpdateConfiguration(configurations);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.IsTrue(objectResult.Value.ToString().Contains("Something went wrong"));
        }
    }
}
