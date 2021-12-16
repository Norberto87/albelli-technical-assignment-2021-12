using System.Collections.Generic;

using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Entities
{
    /// <summary>
    /// Implements a product entity class.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public ProductType Id { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product stack size.
        /// This property is used to calculate the minimum required width of a stack of products.
        /// </summary>
        public int StackSize { get; set; }

        /// <summary>
        /// Gets or sets the product width.
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Gets or sets the related order products.
        /// </summary>
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
