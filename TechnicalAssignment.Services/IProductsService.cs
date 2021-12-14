using System.Collections.Generic;
using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;

namespace TechnicalAssignment.Services
{
    public interface IProductsService
    {
        Task<ProductDto> GetProductAsync(int id);

        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }
}
