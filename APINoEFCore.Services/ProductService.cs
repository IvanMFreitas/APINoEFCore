using APINoEFCore.Data.Repositories.Interface;
using APINoEFCore.Entities.Models;
using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Services.Interface;

namespace APINoEFCore.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public Product GetById(Guid id)
        {
            var product = _productRepository.GetById(id);
            return product;

        }
    }
}