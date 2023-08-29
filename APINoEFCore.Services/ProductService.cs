using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Services.Interface;

namespace APINoEFCore.Services
{
    public class ProductService : IProductService
    {
        Task<ProductViewModel> IProductService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}