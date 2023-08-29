using APINoEFCore.Data.Repositories.Interface;
using APINoEFCore.Entities.Models;
using APINoEFCore.Entities.RequestModels;
using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Services.Interface;

namespace APINoEFCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order GetById(Guid id)
        {
            var order = _orderRepository.GetById(id);
            return order;

        }
        Task<(bool success, string message)> IOrderService.CreateOrder(OrderRequestModel request, string personId)
        {
            throw new NotImplementedException();
        }
    }
}