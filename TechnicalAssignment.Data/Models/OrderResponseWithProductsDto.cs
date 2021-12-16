using System.Collections.Generic;

namespace TechnicalAssignment.Data.Models
{
    /// <summary>
    /// Implements an order with products DTO class to be used when a response from the API is performed.
    /// </summary>
    public class OrderResponseWithProductsDto : OrderResponseDto
    {
        /// <summary>
        /// Gets or sets the products related to the order.
        /// </summary>
        public IEnumerable<OrderResponseProductDto> Products { get; set; }
    }
}
