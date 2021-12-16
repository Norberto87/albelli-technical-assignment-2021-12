using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Entities
{
    public class OrderProduct
    {
        public int OrderId { get; set; }

        public ProductType ProductId { get; set; }

        public int Quantity { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
