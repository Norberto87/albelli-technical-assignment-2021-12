using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<OrderDto> GetAsync(int id)
        {
            var order = await context.Orders.FindAsync(id);

            return mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> GetOrderWithProductsAsync(int id)
        {
            var orderWithProducts = await context.Orders
                .AsNoTracking()
                .Where(o => o.Id == id)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .SingleOrDefaultAsync();

            return mapper.Map<OrderWithProductsDto>(orderWithProducts);
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            IQueryable<Order> orders = context.Orders.AsNoTracking();

            return await mapper.ProjectTo<OrderDto>(orders).ToListAsync();
        }

        public async Task<OrderWithProductsDto> CreateAsync(OrderWithProductsDto order)
        {
            var orderToAdd = mapper.Map<Order>(order);

            orderToAdd.Status = (int)OrderStatusType.Received;

            await context.Orders.AddAsync(orderToAdd);
            await context.OrderProducts.AddRangeAsync(orderToAdd.OrderProducts);

            return order;
        }
    }
}
