using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Models
{
    /// <summary>
    /// Implements an order status DTO class.
    /// </summary>
    public class OrderStatusDto
    {
        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        public OrderStatusType OrderStatus { get; set; }
    }
}
