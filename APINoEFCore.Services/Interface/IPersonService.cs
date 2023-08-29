using APINoEFCore.Entities.Models;
using APINoEFCore.Entities.ViewModels;

namespace APINoEFCore.Services.Interface
{
    public interface IPersonService{
        Person GetById(Guid id);
    }
}
