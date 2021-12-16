using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Models
{
    /// <summary>
    /// Implements an order product DTO class to be used when a request to the API is performed.
    /// </summary>
    public class OrderRequestProductDto
    {
        /// <summary>
        /// Gets or sets the product type.
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Gets or sets the product quantity.
        /// </summary>
        public int Quantity { get; set; }
    }
}
