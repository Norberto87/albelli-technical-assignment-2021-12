using System.Collections.Generic;

namespace TechnicalAssignment.Data.Models
{
    /// <summary>
    /// Implements an order with products DTO class to be used when a request to the API is performed.
    /// </summary>
    public class OrderRequestWithProductsDto : OrderRequestDto
    {
        /// <summary>
        /// Gets or sets the products related to the order.
        /// </summary>
        public IEnumerable<OrderRequestProductDto> Products { get; set; }
    }
}
