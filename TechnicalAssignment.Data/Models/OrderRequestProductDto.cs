using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Models
{
    public class OrderRequestProductDto
    {
        public ProductType ProductType { get; set; }

        public int Quantity { get; set; }
    }
}
