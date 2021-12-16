using System.Collections.Generic;
using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Data.Models.Enums;
using TechnicalAssignment.Data.Persistence;
using TechnicalAssignment.Services.Models;

namespace TechnicalAssignment.Services
{
    /// <summary>
    /// Implements product services.
    /// </summary>
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public ProductsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public async Task<OperationResultWithData<ProductDto>> GetProductAsync(ProductType id)
        {
            var product = await unitOfWork.ProductsRepository.GetAsync(id);

            if (product == null)
            {
                return new OperationResultWithData<ProductDto> { StatusCode = OperationStatusCode.NotFound };
            }

            return new OperationResultWithData<ProductDto>(OperationStatusCode.Ok, product);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await unitOfWork.ProductsRepository.GetAllAsync();
        }
    }
}
