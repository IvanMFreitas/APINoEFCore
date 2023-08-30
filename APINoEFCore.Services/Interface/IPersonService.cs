using APINoEFCore.Entities.Models;
using APINoEFCore.Entities.ViewModels;

namespace APINoEFCore.Services.Interface
{
    public interface IPersonService{
        PersonViewModel GetById(Guid id);
        string GenerateJwtToken(string userEmail, string pwd);
    }
}
