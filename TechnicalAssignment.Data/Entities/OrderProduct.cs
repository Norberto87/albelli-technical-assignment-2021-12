using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Entities
{
    /// <summary>
    /// Implements an order product entity class.
    /// </summary>
    public class OrderProduct
    {
        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public ProductType ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the related order entity.
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// Gets or sets the related product entity.
        /// </summary>
        public virtual Product Product { get; set; }
    }
}
