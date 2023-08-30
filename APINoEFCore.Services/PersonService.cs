using System.Security.Claims;
using System.Text;
using APINoEFCore.Data.Repositories.Interface;
using APINoEFCore.Entities.Models;
using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Services.Interface;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using APINoEFCore.Entities.RequestModels;

namespace APINoEFCore.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public PersonService(IRepository<Person> personRepository, IPasswordService passwordService, IMapper mapper)
        {
            _personRepository = personRepository;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public PersonViewModel GetById(Guid id)
        {
            var person = _personRepository.GetById(id);
            return _mapper.Map<PersonViewModel>(person);
        }

        public PersonViewModel GetByEmail(string email)
        {
            var person = _personRepository.GetWhere(x => x.Email == email).FirstOrDefault();
            return _mapper.Map<PersonViewModel>(person);
        }

        public (bool success, string message) CreatePerson(PersonCreateRequestModel request)
        {
            try
            {
                Func<Person, bool> condition = person => person.Email == request.Email;
                var person = _personRepository.GetWhere(condition).FirstOrDefault();

                if (person != null){
                    return (false, $"The email address {person.Email} was previously used.");
                }

                var (hash, salt) = _passwordService.HashPassword(request.Password);

                var personToAdd = new Person{
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Email = request.Email,
                    PasswordHash = hash,
                    Salt = salt,
                    CreatedAt = DateTime.Now,
                    IsAdmin = request.IsAdmin
                };

                _personRepository.Add(personToAdd);

                return (true, "Person was created successfully");
                
            }
            catch (Exception ex)
            {
                return (false, $"There was an error: {ex.Message}");
            }

        }

        

        public (bool success, string message) UpdatePerson(PersonUpdateRequestModel request, string email)
        {
            try
            {
                if (String.IsNullOrEmpty(email)){
                    return (false, $"You should send a valid email address.");
                }

                Func<Person, bool> condition = person => person.Email == email;
                var person = _personRepository.GetWhere(condition).FirstOrDefault();

                if (person == null){
                    return (false, $"The Person with email address {email} was not found.");
                }

                var personToUpdate = new Person{
                    Id = person.Id,
                    Name = request.Name,
                    Email = person.Email,
                    PasswordHash = person.PasswordHash,
                    Salt = person.Salt,
                    CreatedAt = person.CreatedAt,
                    IsAdmin = request.IsAdmin
                };

                _personRepository.Update(personToUpdate);

                return (true, "Person was updated successfully");
                
            }
            catch (Exception ex)
            {
                return (false, $"There was an error: {ex.Message}");
            }

        }

        public (bool success, string message) DeletePerson(string email)
        {
            try
            {
                if (String.IsNullOrEmpty(email)){
                    return (false, $"You should send a valid email address.");
                }

                Func<Person, bool> condition = person => person.Email == email;
                var person = _personRepository.GetWhere(condition).FirstOrDefault();

                if (person == null){
                    return (false, $"The Person with email address {person.Email} was not found.");
                }

                _personRepository.Delete(person.Id);

                return (true, "Person was deleted successfully");
                
            }
            catch (Exception ex)
            {
                return (false, $"There was an error: {ex.Message}");
            }

        }

        public (bool success, string message) ChangePassword(PersonChangePwdRequestModel request)
        {
            try
            {
                if (String.IsNullOrEmpty(request.Email))
                {
                    return (false, $"You should send a valid email address.");
                }

                if (String.IsNullOrEmpty(request.Password))
                {
                    return (false, $"The password cannot be empty.");
                }

                Func<Person, bool> condition = person => person.Email == request.Email;
                var person = _personRepository.GetWhere(condition).FirstOrDefault();

                if (person == null){
                    return (false, $"The Person with email address {person.Email} was not found.");
                }

                var (hash, salt) = _passwordService.HashPassword(request.Password);

                person.PasswordHash = hash;
                person.Salt = salt;

                _personRepository.Update(person);

                return (true, "The password was changed successfully");
                
            }
            catch (Exception ex)
            {
                return (false, $"There was an error: {ex.Message}");
            }

        }

        public (bool success, string message) Login(string userEmail, string pwd)
        {
            try
            {

                if (String.IsNullOrEmpty(userEmail))
                {
                    return (false, $"You should send a valid email address.");
                }

                Func<Person, bool> condition = person => person.Email == userEmail;
                var person = _personRepository.GetWhere(condition).FirstOrDefault();

                if (person == null){
                    return (false, $"The Person with email address {person.Email} was not found.");
                }

                if (!_passwordService.IsPasswordValid(pwd, person.PasswordHash, person.Salt)){
                    return (false, $"The password is invalid. Unauthorized.");
                }

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9uUemxx8pb3i0Kw3ovb4V1k7bMuyAA9h"));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var listClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.SerialNumber, person.Id.ToString()),
                    new Claim(ClaimTypes.Email, person.Email),
                    new Claim(ClaimTypes.Role, person.IsAdmin ? "admin" : "user")
                };

                var token = new JwtSecurityToken(
                    issuer: "APINoEFCore",
                    audience: "APINoEFCore",
                    claims: listClaims,
                    expires: DateTime.UtcNow.AddDays(1), // Token expiration time
                    signingCredentials: credentials
                );

                var tokenHandler = new JwtSecurityTokenHandler();

                return (true, tokenHandler.WriteToken(token));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}