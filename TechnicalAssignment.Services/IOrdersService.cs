using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Services.Models;

namespace TechnicalAssignment.Services
{
    public interface IOrdersService
    {
        Task<OperationResultWithData<OrderResponseWithProductsDto>> CreateOrderAsync(OrderRequestWithProductsDto order);

        Task<OperationResultWithData<OrderResponseWithProductsDto>> GetOrderWithProductsAsync(int id);

        Task<OperationResultWithData<OrderStatusDto>> GetOrderStatusAsync(int id);

        Task<OperationResult> DeleteOrderAsync(int id);

        Task<OperationResult> UpdateOrderStatusAsync(OrderStatusDto order);
    }
}
