using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Models
{
    public class ProductDto
    {
        public ProductType ProductType { get; set; }

        public string Name { get; set; }

        public float Width { get; set; }
    }
}
