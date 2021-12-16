namespace TechnicalAssignment.Data.Models
{
    /// <summary>
    /// Implements an order DTO class to be used when a request to the API is performed.
    /// This class is also the base class of <see cref="OrderRequestWithProductsDto"/>.
    /// </summary>
    public class OrderRequestDto
    {
        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        public int OrderId { get; set; }
    }
}
