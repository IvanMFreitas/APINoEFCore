using APINoEFCore.Data.Repositories.Interface;
using APINoEFCore.Entities.Models;
using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Services.Interface;

namespace APINoEFCore.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> _personRepository;

        public PersonService(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public Person GetById(Guid id)
        {
            var person = _personRepository.GetById(id);
            return person;

        }
    }
}