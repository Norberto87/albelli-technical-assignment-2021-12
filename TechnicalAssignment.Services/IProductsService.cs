using System.Collections.Generic;
using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Data.Models.Enums;
using TechnicalAssignment.Services.Models;

namespace TechnicalAssignment.Services
{
    public interface IProductsService
    {
        Task<OperationResultWithData<ProductDto>> GetProductAsync(ProductType id);

        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }
}
