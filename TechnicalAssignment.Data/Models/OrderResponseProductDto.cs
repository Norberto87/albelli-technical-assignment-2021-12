using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Models
{
    /// <summary>
    /// Implements an order product DTO class to be used when a response from the API is performed.
    /// </summary>
    public class OrderResponseProductDto
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
        /// Gets or sets the product required bin width.
        /// This property is set according to the product stack size, width and quantity.
        /// </summary>
        public float BinWidth { get; set; }

        /// <summary>
        /// Gets or sets the product width.
        /// </summary>
        public float Width { get; set; }
    }
}
