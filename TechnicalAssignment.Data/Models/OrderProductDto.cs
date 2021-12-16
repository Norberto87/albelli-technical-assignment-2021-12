using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Models
{
    /// <summary>
    /// Implements an order product DTO class.
    /// This class contains all properties from a product.
    /// </summary>
    public class OrderProductDto
    {
        /// <summary>
        /// Gets or sets the product type.
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Gets or sets the product quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the product stack size.
        /// </summary>
        public int StackSize { get; set; }

        /// <summary>
        /// Gets or sets the product width.
        /// </summary>
        public float Width { get; set; }
    }
}
