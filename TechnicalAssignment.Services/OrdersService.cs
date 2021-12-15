using System.Collections.Generic;
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
            var validationResult = ValidateOrderAndProductsData(order);

            if (validationResult.StatusCode != OperationStatusCode.Ok)
            {
                return new OperationResultWithData<OrderWithProductsDto>(validationResult, order);
            }

            if (await unitOfWork.OrdersRepository.GetAsync(order.Id) != null)
            {
                return new OperationResultWithData<OrderWithProductsDto>(OperationStatusCode.AlreadyExists, order);
            }

            var createdOrder = await unitOfWork.OrdersRepository.CreateAsync(order);

            return new OperationResultWithData<OrderWithProductsDto>(OperationStatusCode.Ok, createdOrder);
        }

        public Task<OrderDto> GetOrderAsync(int id)
        {
            return unitOfWork.OrdersRepository.GetAsync(id);
        }

        public Task<OrderDto> GetOrderWithProductsAsync(int id)
        {
            return unitOfWork.OrdersRepository.GetOrderWithProductsAsync(id);
        }

        private OperationResult ValidateOrderAndProductsData(OrderWithProductsDto order)
        {
            var orderValidationResult = ValidateOrderData(order);

            if (orderValidationResult.StatusCode != OperationStatusCode.Ok)
            {
                return orderValidationResult;
            }

            var productsValidationResult = ValidateProductsData(order.Products);

            if (productsValidationResult.StatusCode != OperationStatusCode.Ok)
            {
                return productsValidationResult;
            }

            return new OperationResult(OperationStatusCode.Ok);
        }

        private OperationResult ValidateOrderData(OrderWithProductsDto order)
        {
            if (order == null)
            {
                return new OperationResult(OperationStatusCode.InvalidData, "The order cannot be null");
            }

            if (order.Id <= 0)
            {
                return new OperationResult(OperationStatusCode.InvalidData, "The order ID provided is not valid");
            }

            return new OperationResult(OperationStatusCode.Ok);
        }

        private OperationResult ValidateProductsData(IEnumerable<OrderProductDto> products)
        {
            if (products == null || !products.Any())
            {
                return new OperationResult(OperationStatusCode.InvalidData, "The order must contain at least one product");
            }

            foreach (var product in products)
            {
                if (product.Id <= 0)
                {
                    return new OperationResult(OperationStatusCode.InvalidData, "One or more product IDs are not valid");
                }

                if (product.Quantity < 1)
                {
                    return new OperationResult(OperationStatusCode.InvalidData, "The order products quantity must be a positive value");
                }
            }

            return new OperationResult(OperationStatusCode.Ok);
        }
    }
}
