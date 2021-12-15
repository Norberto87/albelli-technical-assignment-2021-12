using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Services.Models;

namespace TechnicalAssignment.Services
{
    public interface IOrdersService
    {
        Task<OperationResultWithData<OrderWithProductsDto>> CreateOrderAsync(OrderWithProductsDto order);

        Task<OrderDto> GetOrderAsync(int id);

        Task<OrderDto> GetOrderWithProductsAsync(int id);
    }
}
