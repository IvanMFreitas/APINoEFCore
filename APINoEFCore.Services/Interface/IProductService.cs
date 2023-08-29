using APINoEFCore.Entities.Models;
using APINoEFCore.Entities.ViewModels;

namespace APINoEFCore.Services.Interface
{
    public interface IProductService{
        ProductViewModel GetById(Guid id);
    }
}