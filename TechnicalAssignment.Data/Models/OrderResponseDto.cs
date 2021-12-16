using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Models
{
    /// <summary>
    /// Implements an order DTO class to be used when a response from the API is performed.
    /// This class is also the base class of <see cref="OrderResponseWithProductsDto"/>.
    /// </summary>
    public class OrderResponseDto
    {
        public int OrderId { get; set; }

        public OrderStatusType OrderStatus { get; set; }

        public float RequiredBinWidth { get; set; }
    }
}
