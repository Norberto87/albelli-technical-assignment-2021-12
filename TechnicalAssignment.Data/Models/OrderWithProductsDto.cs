using System.Collections.Generic;

namespace TechnicalAssignment.Data.Models
{
    public class OrderWithProductsDto : OrderDto
    {
        public IEnumerable<OrderProductDto> Products { get; set; }
    }
}
