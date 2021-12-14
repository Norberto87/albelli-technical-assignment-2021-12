using System.Collections.Generic;

namespace TechnicalAssignment.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        // TODO: add order status column.

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
