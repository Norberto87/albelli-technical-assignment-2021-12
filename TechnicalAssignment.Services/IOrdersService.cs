using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Services.Models;

namespace TechnicalAssignment.Services
{
    /// <summary>
    /// Provides the interface to be implemented by an orders service.
    /// </summary>
    public interface IOrdersService
    {
        /// <summary>
        /// Creates an order and related products.
        /// </summary>
        /// <param name="order">Order and products data.</param>
        /// <returns>Operation result that contains the created order and products when the operation is successful.</returns>
        Task<OperationResultWithData<OrderResponseWithProductsDto>> CreateOrderAsync(OrderRequestWithProductsDto order);

        /// <summary>
        /// Gets an order with the related products.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <returns>Operation result that contains the requested order and products when the operation is successful.</returns>
        Task<OperationResultWithData<OrderResponseWithProductsDto>> GetOrderWithProductsAsync(int id);

        /// <summary>
        /// Gets the status of an order.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <returns>Operation result that contains the requested order status when the operation is successful.</returns>
        Task<OperationResultWithData<OrderStatusDto>> GetOrderStatusAsync(int id);

        /// <summary>
        /// Deletes an order.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <returns>Operation result that contains the operation status code.</returns>
        Task<OperationResult> DeleteOrderAsync(int id);

        /// <summary>
        /// Updates an order status.
        /// </summary>
        /// <param name="order">Order status.</param>
        /// <returns>Operation result that contains the operation status code.</returns>
        Task<OperationResult> UpdateOrderStatusAsync(OrderStatusDto order);
    }
}
