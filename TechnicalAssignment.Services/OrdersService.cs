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
            var validationResult = await ValidateOrderAndProductsData(order);

            if (validationResult.StatusCode != OperationStatusCode.Ok)
            {
                return new OperationResultWithData<OrderWithProductsDto>(validationResult, order);
            }

            if (await unitOfWork.OrdersRepository.GetAsync(order.Id) != null)
            {
                return new OperationResultWithData<OrderWithProductsDto>(OperationStatusCode.AlreadyExists, order);
            }

            var createdOrder = await unitOfWork.OrdersRepository.CreateAsync(order);

            await unitOfWork.SaveAsync();

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

        private async Task<OperationResult> ValidateOrderAndProductsData(OrderWithProductsDto order)
        {
            var orderValidationResult = await ValidateOrderData(order);

            if (orderValidationResult.StatusCode != OperationStatusCode.Ok)
            {
                return orderValidationResult;
            }

            var productsValidationResult = await ValidateProductsData(order.Products);

            if (productsValidationResult.StatusCode != OperationStatusCode.Ok)
            {
                return productsValidationResult;
            }

            return new OperationResult(OperationStatusCode.Ok);
        }

        private async Task<OperationResult> ValidateOrderData(OrderWithProductsDto order)
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

        private async Task<OperationResult> ValidateProductsData(IEnumerable<OrderProductDto> products)
        {
            if (products == null || !products.Any())
            {
                return new OperationResult(OperationStatusCode.InvalidData, "The order must contain at least one product");
            }

            var availableProductTypes = await unitOfWork.ProductsRepository.GetAllProductTypesAsync();

            foreach (var product in products)
            {
                if(!availableProductTypes.Contains(product.Id))
                {
                    return new OperationResult(OperationStatusCode.InvalidData, "One or more product IDs is not valid");
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
