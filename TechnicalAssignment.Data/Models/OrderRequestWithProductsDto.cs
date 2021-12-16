using System.Collections.Generic;

namespace TechnicalAssignment.Data.Models
{
    public class OrderRequestWithProductsDto : OrderRequestDto
    {
        public IEnumerable<OrderRequestProductDto> Products { get; set; }
    }
}
