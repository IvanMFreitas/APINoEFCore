using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Entities.RequestModels;
using APINoEFCore.Entities.Models;

namespace APINoEFCore.Services.Interface
{
    public interface IOrderService{
        Order GetById(Guid id);
        Task<(bool success, string message)> CreateOrder(OrderRequestModel request, string personId);
    }
}
