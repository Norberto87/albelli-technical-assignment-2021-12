using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Models
{
    public class OrderResponseProductDto
    {
        public ProductType ProductType { get; set; }

        public int Quantity { get; set; }

        public float BinWidth { get; set; }

        public float Width { get; set; }
    }
}
