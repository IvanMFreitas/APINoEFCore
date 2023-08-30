using System.Security.Claims;
using System.Text;
using APINoEFCore.Data.Repositories.Interface;
using APINoEFCore.Entities.Models;
using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Services.Interface;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace APINoEFCore.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IRepository<Person> personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public PersonViewModel GetById(Guid id)
        {
            var person = _personRepository.GetById(id);
            return _mapper.Map<PersonViewModel>(person);

        }

        public string GenerateJwtToken(string userEmail, string pwd)
        {
            Func<Person, bool> condition = person => person.Email == userEmail && person.Password == pwd;
            var person = _personRepository.GetWhere(condition).FirstOrDefault();

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
            return tokenHandler.WriteToken(token);
        }
    }
}