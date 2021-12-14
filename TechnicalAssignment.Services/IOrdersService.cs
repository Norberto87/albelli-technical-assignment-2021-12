using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;

namespace TechnicalAssignment.Services
{
    public interface IOrdersService
    {
        Task<OrderDto> GetOrderAsync(int id);

        Task<OrderDto> GetOrderWithProductsAsync(int id);
    }
}
