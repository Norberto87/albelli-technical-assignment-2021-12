using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Models
{
    public class OrderStatusDto
    {
        public int OrderId { get; set; }

        public OrderStatusType OrderStatus { get; set; }
    }
}
