using APINoEFCore.Data.Repositories.Interface;
using APINoEFCore.Entities.Models;
using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Services.Interface;
using AutoMapper;

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
    }
}