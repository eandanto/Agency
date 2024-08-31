using Agency.API.Controllers;
using Agency.Application.DTOs;
using Agency.Application.Interfaces;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Agency.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserService> _mockUserService;
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UserController(_mockUserService.Object);
        }

        [Test]
        public async Task Register_ValidUser_ReturnsOkResult()
        {
            // Arrange
            var userDto = new UserDto { EmailAddress = "test@example.com", Password = "Password123" };
            _mockUserService.Setup(service => service.Register(It.IsAny<UserDto>())).ReturnsAsync(userDto);

            // Act
            var result = await _controller.Register(userDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnValue = okResult.Value as UserDto;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(userDto.EmailAddress, returnValue.EmailAddress);
        }

        [Test]
        public async Task Register_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("EmailAddress", "Required");

            // Act
            var result = await _controller.Register(new UserDto());

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Register_ExceptionThrown_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var userDto = new UserDto { EmailAddress = "test@example.com", Password = "Password123" };
            _mockUserService.Setup(service => service.Register(It.IsAny<UserDto>())).ThrowsAsync(new System.Exception("Registration failed"));

            // Act
            var result = await _controller.Register(userDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.IsTrue(badRequestResult.Value.ToString().Contains("Registration failed"));
        }

        [Test]
        public async Task Login_ValidCredentials_ReturnsOkWithToken()
        {
            // Arrange
            var loginDto = new LoginDto { EmailAddress = "test@example.com", Password = "password123" };
            var expectedToken = "fake-jwt-token";

            _mockUserService.Setup(service => service.Login(It.IsAny<LoginDto>()))
                .ReturnsAsync(expectedToken);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            // Cast the result to a dynamic type to access the Token property
            dynamic value = okResult.Value;
            Assert.IsNotNull(value);

            // Assert that the token is correct
            Assert.AreEqual("{ Token = fake-jwt-token }", value.ToString());
        }

        [Test]
        public async Task Login_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("EmailAddress", "Required");

            // Act
            var result = await _controller.Login(new LoginDto());

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Login_ExceptionThrown_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var loginDto = new LoginDto { EmailAddress = "test@example.com", Password = "Password123" };
            _mockUserService.Setup(service => service.Login(It.IsAny<LoginDto>())).ThrowsAsync(new System.Exception("Login failed"));

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.IsTrue(badRequestResult.Value.ToString().Contains("Login failed"));
        }
    }
}
