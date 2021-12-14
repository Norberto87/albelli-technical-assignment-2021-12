using System;
using System.Threading.Tasks;
using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Data.Persistence;

namespace TechnicalAssignment.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrdersService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public async Task<int> CreateOrderAsync(OrderDto order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            throw new NotImplementedException();
        }

        public Task<OrderDto> GetOrderAsync(int id)
        {
            return unitOfWork.OrdersRepository.GetAsync(id);
        }

        public Task<OrderDto> GetOrderWithProductsAsync(int id)
        {
            return unitOfWork.OrdersRepository.GetOrderWithProductsAsync(id);
        }
    }
}
