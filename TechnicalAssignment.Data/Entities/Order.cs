using System.Collections.Generic;

using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Entities
{
    /// <summary>
    /// Implements an order entity class.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        public OrderStatusType Status { get; set; }

        /// <summary>
        /// Gets or sets the order products.
        /// </summary>
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
