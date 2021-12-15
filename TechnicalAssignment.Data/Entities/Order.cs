using System.Collections.Generic;

namespace TechnicalAssignment.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int Status { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
