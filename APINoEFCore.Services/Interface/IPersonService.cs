using APINoEFCore.Entities.Models;
using APINoEFCore.Entities.RequestModels;
using APINoEFCore.Entities.ViewModels;

namespace APINoEFCore.Services.Interface
{
    public interface IPersonService{
        PersonViewModel GetById(Guid id);
        public (bool success, string message) Login(string userEmail, string pwd);
        (bool success, string message) CreatePerson(PersonCreateRequestModel request);
        (bool success, string message) UpdatePerson(PersonUpdateRequestModel request, string email);
        (bool success, string message) DeletePerson(string email);
        (bool success, string message) ChangePassword(PersonChangePwdRequestModel request);
    }
}
