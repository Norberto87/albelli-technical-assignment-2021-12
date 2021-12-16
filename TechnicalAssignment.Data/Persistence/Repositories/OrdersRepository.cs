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
    /// Implements an orders repository.
    /// </summary>
    internal class OrdersRepository : IOrdersRepository
    {
        private readonly ITechnicalAssignmentDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersRepository"/> class.
        /// </summary>
        /// <param name="context">DB context.</param>
        /// <param name="mapper">AutoMapper data mapper.</param>
        public OrdersRepository(ITechnicalAssignmentDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<OrderRequestDto> GetAsync(int id)
        {
            var order = await context.Orders.FindAsync(id);

            return mapper.Map<OrderRequestDto>(order);
        }

        /// <inheritdoc/>
        public async Task<OrderStatusDto> GetStatusAsync(int id)
        {
            var order = await context.Orders.FindAsync(id);

            return mapper.Map<OrderStatusDto>(order);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<OrderProductDto>> GetOrderProductsAsync(int id)
        {
            var orderProducts = await context.OrderProducts
                .AsNoTracking()
                .Where(o => o.OrderId == id)
                .Include(op => op.Product)
                .ToListAsync();

            return mapper.Map<IEnumerable<OrderProductDto>>(orderProducts);
        }

        /// <inheritdoc/>
        public async Task<OrderResponseWithProductsDto> GetOrderWithProductsAsync(int id)
        {
            var orderWithProducts = await context.Orders
                .AsNoTracking()
                .Where(o => o.Id == id)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .SingleOrDefaultAsync();

            return mapper.Map<OrderResponseWithProductsDto>(orderWithProducts);
        }

        /// <inheritdoc/>
        public async Task<OrderResponseWithProductsDto> CreateAsync(OrderRequestWithProductsDto order)
        {
            context.SetBulkMode(true);

            var orderToAdd = mapper.Map<Order>(order);

            orderToAdd.Status = OrderStatusType.Received;

            await context.Orders.AddAsync(orderToAdd);
            await context.OrderProducts.AddRangeAsync(orderToAdd.OrderProducts);

            return mapper.Map<OrderResponseWithProductsDto>(order);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            context.SetBulkMode(true);

            var orderProducts = await context.OrderProducts.Where(op => op.OrderId == id).ToListAsync();

            foreach (var product in orderProducts)
            {
                if (context.Entry(product).State == EntityState.Detached)
                {
                    context.OrderProducts.Attach(product);
                }

                context.OrderProducts.Remove(product);
            }

            var order = await context.Orders.SingleAsync(o => o.Id == id);

            if(context.Entry(order).State == EntityState.Detached)
            {
                context.Orders.Attach(order);
            }

            context.Orders.Remove(order);
        }

        /// <inheritdoc/>
        public async Task UpdateStatusAsync(OrderStatusDto order)
        {
            var entity = await context.Orders.SingleAsync(o => o.Id == order.OrderId);

            entity = mapper.Map(order, entity);

            context.Orders.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
