using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using TechnicalAssignment.Data.Entities;
using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Persistence.Repositories
{
    /// <summary>
    /// Implements a products repository.
    /// </summary>
    internal class ProductsRepository : IProductsRepository
    {
        private readonly ITechnicalAssignmentDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsRepository"/> class.
        /// </summary>
        /// <param name="context">DB context.</param>
        /// <param name="mapper">AutoMapper data mapper.</param>
        public ProductsRepository(ITechnicalAssignmentDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<ProductDto> GetAsync(ProductType id)
        {
            Product product = await context.Products.FindAsync(id);

            return mapper.Map<ProductDto>(product);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            IQueryable<Product> products = context.Products.AsNoTracking();

            return await mapper.ProjectTo<ProductDto>(products).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<HashSet<ProductType>> GetAllProductTypesAsync()
        {
            IQueryable<ProductType> productIds = context.Products.AsNoTracking().Select(p => p.Id);

            return new HashSet<ProductType>(await productIds.ToListAsync());
        }
    }
}
