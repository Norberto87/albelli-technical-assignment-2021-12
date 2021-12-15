using System.Collections.Generic;

namespace TechnicalAssignment.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StackSize { get; set; }

        public float Width { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
