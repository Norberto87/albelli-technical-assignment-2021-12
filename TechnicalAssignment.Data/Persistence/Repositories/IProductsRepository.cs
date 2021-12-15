using System.Collections.Generic;
using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;

namespace TechnicalAssignment.Data.Persistence.Repositories
{
    public interface IProductsRepository
    {
        Task<ProductDto> GetAsync(int id);

        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<HashSet<int>> GetAllProductTypesAsync();
    }
}
