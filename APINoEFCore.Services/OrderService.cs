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
        private readonly IMapper _mapper;

        public OrderService(IRepository<Order> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public OrderViewModel GetById(Guid id)
        {
            var order = _orderRepository.GetById(id);
            return _mapper.Map<OrderViewModel>(order);

        }
        Task<(bool success, string message)> IOrderService.CreateOrder(OrderRequestModel request, string personId)
        {
            throw new NotImplementedException();
        }
    }
}