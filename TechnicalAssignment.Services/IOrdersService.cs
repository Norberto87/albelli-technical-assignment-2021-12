using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Services.Models;

namespace TechnicalAssignment.Services
{
    public interface IOrdersService
    {
        Task<OperationResultWithData<OrderResponseWithProductsDto>> CreateOrderAsync(OrderRequestWithProductsDto order);

        Task<OrderRequestDto> GetOrderAsync(int id);

        Task<OrderRequestDto> GetOrderWithProductsAsync(int id);
    }
}
