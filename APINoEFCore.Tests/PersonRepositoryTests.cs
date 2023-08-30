using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APINoEFCore.Data.Repositories;
using APINoEFCore.Entities.Models;
using NUnit.Framework;

namespace APINoEFCore.Tests.Data
{
    [TestFixture]
    public class PersonRepositoryTests
    {
        private IDbConnection _connection;
        private Repository<Person> _personRepository;

        [SetUp]
        public void Setup()
        {
            // Set up database connection and opens the connection
            _connection = new SqlConnection("Server=localhost;Database=APINoEFCore;User Id=sa;Password=ab123CD*;");
            _connection.Open();

            // Initialize the repository with the database connection
            _personRepository = new Repository<Person>(_connection);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up resources
            _connection.Close();
            _connection.Dispose();
        }

        [Test]
        public void GetById_ValidId_ReturnsEntity()
        {
            // Arrange: Insert a test user into the database
            var guid = Guid.NewGuid();

            var personTest = new Person
            {
                Id = guid,
                Name = "Test User",
                Email = "test@test.com",
                PasswordHash = "123456",
                Salt = "123456",
                IsAdmin = true,
                CreatedAt = DateTime.Now
            };

            _personRepository.Add(personTest);

            // Act: Retrieve the user by ID
            var person = _personRepository.GetById(guid);

            // Delete Entity to remove trash
            _personRepository.Delete(guid);

            // Assert: Check if the user is not null
            Assert.NotNull(person);
        }

        [Test]
        public void GetById_InvalidId_ReturnsNull()
        {
            // Act: Retrieve a user with an invalid ID
            var person = _personRepository.GetById(Guid.NewGuid());

            // Assert: Check if the user is null
            Assert.Null(person);
        }

        [Test]
        public void GetAll_ReturnsListOfEntities()
        {
            // Act: Retrieve all users
            var personList = _personRepository.GetAll();

            // Assert: Check if the list is not empty
            Assert.IsNotEmpty(personList);
        }

        [Test]
        public void GetWhere_ValidCondition_ReturnsFilteredEntities()
        {
            // Arrange: Insert test data into the database
            var guid = Guid.NewGuid();

            var personTest = new Person
            {
                Id = guid,
                Name = "Test User",
                Email = "test@test.com",
                PasswordHash = "123456",
                Salt = "123456",
                IsAdmin = true,
                CreatedAt = DateTime.Now
            };

            // Act: Add the user to the database
            _personRepository.Add(personTest);

            // Act: Retrieve users where a condition is met
            var personList = _personRepository.GetWhere(u => u.Email == "test@test.com");

            // Delete Entity to remove trash
            _personRepository.Delete(guid);

            // Assert: Check if the list is not empty
            Assert.IsNotEmpty(personList);
        }

        [Test]
        public void Add_ValidEntity_InsertsIntoDatabase()
        {
            // Arrange: Create a new user entity
            var guid = Guid.NewGuid();

            var personTest = new Person
            {
                Id = guid,
                Name = "Test User",
                Email = "test@test.com",
                PasswordHash = "123456",
                Salt = "123456",
                IsAdmin = true,
                CreatedAt = DateTime.Now
            };

            // Act: Add the user to the database
            _personRepository.Add(personTest);

            // Assert: Retrieve the user and check if it exists in the database
            var retrievedPerson = _personRepository.GetById(guid);

            // Delete Entity to remove trash
            _personRepository.Delete(guid);

            Assert.NotNull(retrievedPerson);
        }

        [Test]
        public void Update_ValidEntity_UpdatesInDatabase()
        {
            // Arrange: Insert a test user into the database
            var guid = Guid.NewGuid();

            var personTest = new Person
            {
                Id = guid,
                Name = "Test User",
                Email = "test@test.com",
                PasswordHash = "123456",
                Salt = "123456",
                IsAdmin = true,
                CreatedAt = DateTime.Now
            };

            _personRepository.Add(personTest);

            // Act: Update the user's properties
            personTest.Name = "New Name";
            _personRepository.Update(personTest);

            // Assert: Retrieve the user and check if the property is updated
            var retrievePerson = _personRepository.GetById(guid);

            // Delete Entity to remove trash
            _personRepository.Delete(guid);

            Assert.That(retrievePerson.Name, Is.EqualTo("New Name"));
        }

        [Test]
        public void Delete_ValidId_RemovesFromDatabase()
        {
            // Arrange: Insert a test user into the database
            var guid = Guid.NewGuid();

            var personTest = new Person
            {
                Id = guid,
                Name = "Test User",
                Email = "test@test.com",
                PasswordHash = "123456",
                Salt = "123456",
                IsAdmin = true,
                CreatedAt = DateTime.Now
            };

            _personRepository.Add(personTest);

            // Act: Delete the user by ID
            _personRepository.Delete(guid);

            // Assert: Try to retrieve the user, it should be null
            var retrievedPerson = _personRepository.GetById(guid);

            // Delete Entity to remove trash
            _personRepository.Delete(guid);

            Assert.Null(retrievedPerson);
        }
    }
}
