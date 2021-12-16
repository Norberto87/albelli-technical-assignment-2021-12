using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Models
{
    public class OrderProductDto
    {
        public ProductType ProductType { get; set; }

        public int Quantity { get; set; }

        public int StackSize { get; set; }

        public float Width { get; set; }
    }
}
