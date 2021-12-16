using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Models
{
    /// <summary>
    /// Implements a product DTO class.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Gets or sets the product type.
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product width.
        /// </summary>
        public float Width { get; set; }
    }
}
