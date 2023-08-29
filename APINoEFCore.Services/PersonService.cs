using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Services.Interface;

namespace APINoEFCore.Services
{
    public class PersonService : IPersonService
    {
        Task<PersonViewModel> IPersonService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}