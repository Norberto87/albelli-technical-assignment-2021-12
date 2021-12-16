using System.Collections.Generic;
using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;

namespace TechnicalAssignment.Data.Persistence.Repositories
{
    public interface IOrdersRepository
    {
        Task<OrderRequestDto> GetAsync(int id);

        Task<OrderStatusDto> GetStatusAsync(int id);

        Task<IEnumerable<OrderProductDto>> GetOrderProductsAsync(int id);

        Task<OrderResponseWithProductsDto> GetOrderWithProductsAsync(int id);

        Task<OrderResponseWithProductsDto> CreateAsync(OrderRequestWithProductsDto order);

        Task DeleteAsync(int id);

        Task UpdateStatusAsync(OrderStatusDto order);
    }
}
