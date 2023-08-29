using APINoEFCore.Data.Repositories.Interface;
using APINoEFCore.Entities.Models;
using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Services.Interface;
using AutoMapper;

namespace APINoEFCore.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ProductViewModel GetById(Guid id)
        {
            var product = _productRepository.GetById(id);
            return _mapper.Map<ProductViewModel>(product);

        }
    }
}