using System.Collections.Generic;
using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Data.Persistence;

namespace TechnicalAssignment.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> GetProductAsync(int id)
        {
            return await unitOfWork.ProductsRepository.GetAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await unitOfWork.ProductsRepository.GetAllAsync();
        }
    }
}
