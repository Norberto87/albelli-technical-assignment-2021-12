using System.Collections.Generic;
using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Data.Models.Enums;
using TechnicalAssignment.Data.Persistence;
using TechnicalAssignment.Services.Models;

namespace TechnicalAssignment.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResultWithData<ProductDto>> GetProductAsync(ProductType id)
        {
            var product = await unitOfWork.ProductsRepository.GetAsync(id);

            if (product == null)
            {
                return new OperationResultWithData<ProductDto> { StatusCode = OperationStatusCode.NotFound };
            }

            return new OperationResultWithData<ProductDto>(OperationStatusCode.Ok, product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await unitOfWork.ProductsRepository.GetAllAsync();
        }
    }
}
