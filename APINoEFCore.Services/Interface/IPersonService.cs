using APINoEFCore.Entities.ViewModels;

namespace APINoEFCore.Services.Interface
{
    public interface IPersonService{
        Task<PersonViewModel> GetByIdAsync(Guid id);
    }
}
