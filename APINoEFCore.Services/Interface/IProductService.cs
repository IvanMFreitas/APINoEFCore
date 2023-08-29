using APINoEFCore.Entities.ViewModels;

namespace APINoEFCore.Services.Interface
{
    public interface IProductService{
        Task<ProductViewModel> GetByIdAsync(Guid id);
    }
}