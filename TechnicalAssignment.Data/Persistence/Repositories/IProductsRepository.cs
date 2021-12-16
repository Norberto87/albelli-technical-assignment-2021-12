using System.Collections.Generic;
using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Persistence.Repositories
{
    /// <summary>
    /// Provides the interface to implemented by a products repository.
    /// </summary>
    public interface IProductsRepository
    {
        /// <summary>
        /// Gets a single product.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns>Product that matches the ID.</returns>
        Task<ProductDto> GetAsync(ProductType id);

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>Collection of all products.</returns>
        Task<IEnumerable<ProductDto>> GetAllAsync();

        /// <summary>
        /// Gets all product types.
        /// </summary>
        /// <returns>Collection of all product types. The collection is a <see cref="HashSet{T}"/>, intended to be used for O(1) search operations.</returns>
        Task<HashSet<ProductType>> GetAllProductTypesAsync();
    }
}
