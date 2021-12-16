using System.Collections.Generic;
using System.Threading.Tasks;

using TechnicalAssignment.Data.Models;

namespace TechnicalAssignment.Data.Persistence.Repositories
{
    /// <summary>
    /// Provides the interface to implemented by an orders repository.
    /// </summary>
    public interface IOrdersRepository
    {
        /// <summary>
        /// Gets an order.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <returns>Order that matches the ID.</returns>
        Task<OrderRequestDto> GetAsync(int id);

        /// <summary>
        /// Gets the status of an order.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <returns>Order status that matches the ID.</returns>
        Task<OrderStatusDto> GetStatusAsync(int id);

        /// <summary>
        /// Gets the products of an order.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <returns>Collection of products.</returns>
        Task<IEnumerable<OrderProductDto>> GetOrderProductsAsync(int id);

        /// <summary>
        /// Gets an order with the related products.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <returns>Order with collection of products.</returns>
        Task<OrderResponseWithProductsDto> GetOrderWithProductsAsync(int id);

        /// <summary>
        /// Creates an order and related products.
        /// </summary>
        /// <param name="order">Order and products data.</param>
        /// <returns>Created order and products.</returns>
        Task<OrderResponseWithProductsDto> CreateAsync(OrderRequestWithProductsDto order);

        /// <summary>
        /// Deletes an order.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <returns>The task returned represents the asynchronous operation.</returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Updates an order status.
        /// </summary>
        /// <param name="order">Order status.</param>
        /// <returns>The task returned represents the asynchronous operation.</returns>
        Task UpdateStatusAsync(OrderStatusDto order);
    }
}
