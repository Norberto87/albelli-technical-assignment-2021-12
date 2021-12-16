using System.Collections.Generic;
using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Data.Models.Enums;
using TechnicalAssignment.Services.Models;

namespace TechnicalAssignment.Services
{
    public interface IProductsService
    {
        /// <summary>
        /// Gets a single product.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns>Operation result that contains the requested product when the operation is successful.</returns>
        Task<OperationResultWithData<ProductDto>> GetProductAsync(ProductType id);

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>Operation result that contains the collection of all products when the operation is successful.</returns>
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }
}
