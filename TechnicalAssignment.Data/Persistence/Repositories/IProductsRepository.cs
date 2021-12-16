using System.Collections.Generic;
using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Persistence.Repositories
{
    public interface IProductsRepository
    {
        Task<ProductDto> GetAsync(ProductType id);

        Task<ProductDto> GetAsync(string name);

        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<HashSet<ProductType>> GetAllProductTypesAsync();
    }
}
