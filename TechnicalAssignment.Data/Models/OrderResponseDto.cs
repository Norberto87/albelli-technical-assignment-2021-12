using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Models
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }

        public OrderStatusType OrderStatus { get; set; }

        public float RequiredBinWidth { get; set; }
    }
}
