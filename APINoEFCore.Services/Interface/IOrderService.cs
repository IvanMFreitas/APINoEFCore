using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Entities.RequestModels;

namespace APINoEFCore.Services.Interface
{
    public interface IOrderService{
        Task<OrderViewModel> GetByIdAsync(Guid id);
        Task<(bool success, string message)> CreateOrder(OrderRequestModel request, string personId);
    }
}
