using System;
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
        public async Task<OperationResultWithData<OrderResponseWithProductsDto>> CreateOrderAsync(OrderRequestWithProductsDto order)
        {
            var validationResult = await ValidateOrderAndProductsData(order);

            if (validationResult.StatusCode != OperationStatusCode.Ok)
            {
                return new OperationResultWithData<OrderResponseWithProductsDto>(validationResult);
            }

            if (await unitOfWork.OrdersRepository.GetAsync(order.Id) != null)
            {
                return new OperationResultWithData<OrderResponseWithProductsDto> { StatusCode = OperationStatusCode.AlreadyExists };
            }

            var createdOrder = await unitOfWork.OrdersRepository.CreateAsync(order);

            await unitOfWork.SaveAsync();

            return new OperationResultWithData<OrderResponseWithProductsDto>(OperationStatusCode.Ok, await ProcessOrderProducts(createdOrder));
        }

        /// <inheritdoc/>
        public async Task<OperationResultWithData<OrderResponseWithProductsDto>> GetOrderWithProductsAsync(int id)
        {
            var order = await unitOfWork.OrdersRepository.GetOrderWithProductsAsync(id);

            if (order == null)
            {
                return new OperationResultWithData<OrderResponseWithProductsDto> { StatusCode = OperationStatusCode.NotFound };
            }

            return new OperationResultWithData<OrderResponseWithProductsDto>(OperationStatusCode.Ok, await ProcessOrderProducts(order));
        }

        /// <inheritdoc/>
        public async Task<OperationResultWithData<OrderStatusDto>> GetOrderStatusAsync(int id)
        {
            var orderStatus = await unitOfWork.OrdersRepository.GetStatusAsync(id);

            if(orderStatus == null)
            {
                return new OperationResultWithData<OrderStatusDto> { StatusCode = OperationStatusCode.NotFound };
            }

            return new OperationResultWithData<OrderStatusDto>(OperationStatusCode.Ok, orderStatus);
        }

        /// <inheritdoc/>
        public async Task<OperationResult> DeleteOrderAsync(int id)
        {
            if (id <= 0)
            {
                return new OperationResult(OperationStatusCode.InvalidData, "The order ID provided is not valid");
            }

            if (await unitOfWork.OrdersRepository.GetAsync(id) == null)
            {
                return new OperationResult(OperationStatusCode.NotFound, "The order ID provided does not exist");
            }

            await unitOfWork.OrdersRepository.DeleteAsync(id);

            await unitOfWork.SaveAsync();

            return new OperationResult(OperationStatusCode.Ok);
        }

        /// <inheritdoc/>
        public async Task<OperationResult> UpdateOrderStatusAsync(OrderStatusDto order)
        {
            if (order.Id <= 0)
            {
                return new OperationResult(OperationStatusCode.InvalidData, "The order ID provided is not valid");
            }

            if (await unitOfWork.OrdersRepository.GetAsync(order.Id) == null)
            {
                return new OperationResult(OperationStatusCode.NotFound, "The order does not exist");
            }

            await unitOfWork.OrdersRepository.UpdateStatusAsync(order);

            await unitOfWork.SaveAsync();

            return new OperationResult(OperationStatusCode.Ok);
        }

        private async Task<OrderResponseWithProductsDto> ProcessOrderProducts(OrderResponseWithProductsDto order)
        {
            IEnumerable<OrderProductDto> orderProducts = await unitOfWork.OrdersRepository.GetOrderProductsAsync(order.Id);

            order.Products = new List<OrderResponseProductDto>(orderProducts.Select(p =>
            {
                OrderResponseProductDto product = new OrderResponseProductDto
                {
                    Id = p.Id,
                    Quantity = p.Quantity,
                    RequiredWidth = p.Width * GetStacks(p.Quantity, p.StackSize)
                };

                order.RequiredWidth += product.RequiredWidth;

                return product;
            }).ToList());

            return order;
        }

        private int GetStacks(int quantity, int stackSize)
        {
            return stackSize == 1
                ? quantity
                : (int)Math.Ceiling((float)quantity / stackSize);
        }

        private async Task<OperationResult> ValidateOrderAndProductsData(OrderRequestWithProductsDto order)
        {
            var orderValidationResult = ValidateOrderData(order);

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

        private OperationResult ValidateOrderData(OrderRequestWithProductsDto order)
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

        private async Task<OperationResult> ValidateProductsData(IEnumerable<OrderRequestProductDto> products)
        {
            if (products == null || !products.Any())
            {
                return new OperationResult(OperationStatusCode.InvalidData, "The order must contain at least one product");
            }

            var availableProductTypes = await unitOfWork.ProductsRepository.GetAllProductTypesAsync();

            foreach (var product in products)
            {
                if (!availableProductTypes.Contains(product.Id))
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
