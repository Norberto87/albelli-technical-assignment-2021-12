using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalAssignment.Data.Models;

namespace TechnicalAssignment.Data.Persistence.Repositories
{
    public interface IOrdersRepository
    {
        Task<OrderRequestDto> GetAsync(int id);

        Task<IEnumerable<OrderProductDto>> GetOrderProductsAsync(int id);

        Task<OrderRequestDto> GetOrderWithProductsAsync(int id);

        Task<OrderResponseWithProductsDto> CreateAsync(OrderRequestWithProductsDto order);
    }
}
