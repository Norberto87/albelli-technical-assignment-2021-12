using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Models
{
    public class OrderStatusDto
    {
        public int Id { get; set; }

        public OrderStatusType Status { get; set; }
    }
}
