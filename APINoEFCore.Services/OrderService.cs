using System.Data.SqlClient;
using APINoEFCore.Data.Repositories.Interface;
using APINoEFCore.Entities.Models;
using APINoEFCore.Entities.RequestModels;
using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Services.Interface;
using AutoMapper;

namespace APINoEFCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public OrderService(IRepository<Order> orderRepository, IRepository<Product> productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public OrderViewModel GetById(Guid id)
        {
            var order = _orderRepository.GetById(id);
            return _mapper.Map<OrderViewModel>(order);

        }
        public (bool success, string message) CreateOrder(OrderRequestModel request, string personId)
        {
            try
            {
                if (request.Qty <= 0){
                    return (false, "The quantity should have a positive value");
                }

                var product = _productRepository.GetById(Guid.Parse(request.ProductId));

                if (product == null){
                    return (false, "The product doesn't exist");
                }

                var parameters = new[]
                {
                    new SqlParameter("@PersonId", personId),
                    new SqlParameter("@ProductId", request.ProductId),
                    new SqlParameter("@Qty", request.Qty)
                };

                _orderRepository.ExecuteStoredProcedure("CreateOrder", parameters);

                return (true, "The order was created successfully");
            }
            catch (Exception ex)
            {
                return (false, $"There was an error: {ex.Message}");
            }
        }
    }
}