using System.Collections.Generic;

using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Entities
{
    public class Product
    {
        public ProductType Id { get; set; }

        public string Name { get; set; }

        public int StackSize { get; set; }

        public float Width { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
