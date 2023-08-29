using APINoEFCore.Entities.Models;
using APINoEFCore.Entities.ViewModels;

namespace APINoEFCore.Services.Interface
{
    public interface IProductService{
        Product GetById(Guid id);
    }
}