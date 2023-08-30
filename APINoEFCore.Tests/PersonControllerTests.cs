using Moq;
using APINoEFCore.Services.Interface;
using APINoEFCore.Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using APINoEFCore.API.Controllers;

namespace APINoEFCore.Tests.Controllers
{
    [TestFixture]
    public class PersonControllerTests
    {
        private PersonController _personController;
        private Mock<IPersonService> _personServiceMock;

        [SetUp]
        public void Setup()
        {
            _personServiceMock = new Mock<IPersonService>();
            _personController = new PersonController(_personServiceMock.Object);
        }

        [Test]
        public void GetPersonById_ValidId_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var person = new PersonViewModel
            {
                Id = id.ToString()
            };
            _personServiceMock.Setup(service => service.GetById(id)).Returns(person);

            // Act
            var result = _personController.GetPersonById(id) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetPersonByEmail_ValidId_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var person = new PersonViewModel
            {
                Id = id.ToString(),
                Email = "test@test.com"
            };
            _personServiceMock.Setup(service => service.GetByEmail(person.Email)).Returns(person);

            // Act
            var result = _personController.GetPersonByEmail(person.Email) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetPersonById_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _personServiceMock.Setup(service => service.GetById(id)).Returns((PersonViewModel)null);

            // Act
            var result = _personController.GetPersonById(id) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task Login_ValidCredentials_ReturnsOk()
        {
            // Arrange
            var email = "test@example.com";
            var pwd = "password";
            var success = true;
            var message = "Login successful";
            _personServiceMock.Setup(service => service.Login(email, pwd)).Returns((success, message));

            // Act
            var result = await _personController.Login(email, pwd) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
    }
}