using APINoEFCore.Entities.RequestModels;
using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Services.Interface;

namespace APINoEFCore.Services
{
    public class OrderService : IOrderService
    {
        Task<(bool success, string message)> IOrderService.CreateOrder(OrderRequestModel request, string personId)
        {
            throw new NotImplementedException();
        }

        Task<OrderViewModel> IOrderService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}