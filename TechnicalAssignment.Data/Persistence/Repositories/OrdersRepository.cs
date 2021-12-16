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
    internal class OrdersRepository : IOrdersRepository
    {
        private readonly ITechnicalAssignmentDbContext context;
        private readonly IMapper mapper;

        public OrdersRepository(ITechnicalAssignmentDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<OrderRequestDto> GetAsync(int id)
        {
            var order = await context.Orders.FindAsync(id);

            return mapper.Map<OrderRequestDto>(order);
        }

        public async Task<OrderStatusDto> GetStatusAsync(int id)
        {
            var order = await context.Orders.FindAsync(id);

            return mapper.Map<OrderStatusDto>(order);
        }

        public async Task<IEnumerable<OrderProductDto>> GetOrderProductsAsync(int id)
        {
            var orderProducts = await context.OrderProducts
                .AsNoTracking()
                .Where(o => o.OrderId == id)
                .Include(op => op.Product)
                .ToListAsync();

            return mapper.Map<IEnumerable<OrderProductDto>>(orderProducts);
        }

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

        public async Task<IEnumerable<OrderRequestDto>> GetAllAsync()
        {
            IQueryable<Order> orders = context.Orders.AsNoTracking();

            return await mapper.ProjectTo<OrderRequestDto>(orders).ToListAsync();
        }

        public async Task<OrderResponseWithProductsDto> CreateAsync(OrderRequestWithProductsDto order)
        {
            context.SetBulkMode(true);

            var orderToAdd = mapper.Map<Order>(order);

            orderToAdd.Status = (int)OrderStatusType.Received;

            await context.Orders.AddAsync(orderToAdd);
            await context.OrderProducts.AddRangeAsync(orderToAdd.OrderProducts);

            return mapper.Map<OrderResponseWithProductsDto>(order);
        }

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

        public async Task UpdateStatusAsync(OrderStatusDto order)
        {
            var entity = await context.Orders.SingleAsync(o => o.Id == order.OrderId);

            entity = mapper.Map(order, entity);

            context.Orders.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
