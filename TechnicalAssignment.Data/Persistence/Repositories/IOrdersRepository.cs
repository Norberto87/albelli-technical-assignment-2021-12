using System.Threading.Tasks;
using TechnicalAssignment.Data.Models;

namespace TechnicalAssignment.Data.Persistence.Repositories
{
    public interface IOrdersRepository
    {
        Task<OrderDto> GetAsync(int id);

        Task<OrderDto> GetOrderWithProductsAsync(int id);

        Task<OrderWithProductsDto> CreateAsync(OrderWithProductsDto order);
    }
}
