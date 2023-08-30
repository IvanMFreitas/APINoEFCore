using System;
using APINoEFCore.Data.Repositories.Interface;
using APINoEFCore.Entities.Models;
using APINoEFCore.Entities.RequestModels;
using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Services;
using APINoEFCore.Services.Interface;
using AutoMapper;
using Moq;

namespace APINoEFCore.Tests.Services
{
    public class PersonServiceTests
    {
        private Mock<IRepository<Person>> _personRepositoryMock;
        private Mock<IPasswordService> _passwordServiceMock;
        private Mock<IMapper> _mapperMock;
        private PersonService _personService;

        [SetUp]
        public void Setup()
        {
            _personRepositoryMock = new Mock<IRepository<Person>>();
            _passwordServiceMock = new Mock<IPasswordService>();
            _mapperMock = new Mock<IMapper>();
            _personService = new PersonService(_personRepositoryMock.Object, _passwordServiceMock.Object, _mapperMock.Object);
        }

        [Test]
        public void GetById_ValidId_ReturnsPersonViewModel()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var expectedPerson = new Person { Id = personId };
            _personRepositoryMock.Setup(repo => repo.GetById(personId)).Returns(expectedPerson);
            _mapperMock.Setup(mapper => mapper.Map<PersonViewModel>(expectedPerson)).Returns(new PersonViewModel());

            // Act
            var result = _personService.GetById(personId);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<PersonViewModel>(result);
        }

        [Test]
        public void CreatePerson_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var request = new PersonCreateRequestModel();
            _passwordServiceMock.Setup(passwordService => passwordService.HashPassword(It.IsAny<string>())).Returns((string.Empty, string.Empty));

            // Act
            var result = _personService.CreatePerson(request);

            // Assert
            Assert.True(result.success);
            Assert.That(result.message, Is.EqualTo("Person was created successfully"));
        }

        [Test]
        public void UpdatePerson_ValidRequestAndEmail_ReturnsSuccess()
        {
            // Arrange
            var request = new PersonUpdateRequestModel();
            var email = "test@example.com";
            var existingPerson = new Person { Email = email };
            _personRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Func<Person, bool>>())).Returns(new[] { existingPerson });

            // Act
            var result = _personService.UpdatePerson(request, email);

            // Assert
            Assert.True(result.success);
            Assert.That(result.message, Is.EqualTo("Person was updated successfully"));
        }

        [Test]
        public void DeletePerson_ValidEmail_ReturnsSuccess()
        {
            // Arrange
            var email = "test@example.com";
            var existingPerson = new Person { Email = email };
            _personRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Func<Person, bool>>())).Returns(new[] { existingPerson });

            // Act
            var result = _personService.DeletePerson(email);

            // Assert
            Assert.True(result.success);
            Assert.That(result.message, Is.EqualTo("Person was deleted successfully"));
        }
    }
}

