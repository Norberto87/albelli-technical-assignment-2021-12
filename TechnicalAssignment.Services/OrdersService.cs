using System;
using System.Linq;
using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Data.Persistence;
using TechnicalAssignment.Services.Models;

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
        public async Task<OperationResultWithData<OrderWithProductsDto>> CreateOrderAsync(OrderWithProductsDto order)
        {
            if (order == null)
            {
                return new OperationResultWithData<OrderWithProductsDto>
                {
                    StatusCode = OperationStatusCode.InvalidData,
                    Message = "The order cannot be null",
                    Data = order
                };
            }

            if (order.Id <= 0)
            {
                return new OperationResultWithData<OrderWithProductsDto>
                {
                    StatusCode = OperationStatusCode.InvalidData,
                    Message = "The order ID provided is not valid"
                };
            }

            if (order.Products == null || !order.Products.Any())
            {
                return new OperationResultWithData<OrderWithProductsDto>
                {
                    StatusCode = OperationStatusCode.InvalidData,
                    Message = "The order must contain at least one product",
                    Data = order
                };
            }

            if (await unitOfWork.OrdersRepository.GetAsync(order.Id) != null)
            {
                return new OperationResultWithData<OrderWithProductsDto>
                {
                    StatusCode = OperationStatusCode.AlreadyExists,
                    Data = order
                };
            }

            var createdOrder = await unitOfWork.OrdersRepository.CreateAsync(order);

            return new OperationResultWithData<OrderWithProductsDto>
            {
                StatusCode = OperationStatusCode.Ok,
                Data = createdOrder
            };
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
