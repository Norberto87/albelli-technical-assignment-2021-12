using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using TechnicalAssignment.Data.Entities;
using TechnicalAssignment.Data.Models;

namespace TechnicalAssignment.Data.Persistence.Repositories
{
    internal class ProductsRepository : IProductsRepository
    {
        private readonly ITechnicalAssignmentDbContext context;
        private readonly IMapper mapper;

        public ProductsRepository(ITechnicalAssignmentDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ProductDto> GetAsync(int id)
        {
            Product product = await context.Products.FindAsync(id);

            return mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            IQueryable<Product> products = context.Products.AsNoTracking();

            return await mapper.ProjectTo<ProductDto>(products).ToListAsync();
        }

        public async Task<HashSet<int>> GetAllProductTypesAsync()
        {
            IQueryable<int> productIds = context.Products.AsNoTracking().Select(p => p.Id);

            return new HashSet<int>(await productIds.ToListAsync());
        }
    }
}
