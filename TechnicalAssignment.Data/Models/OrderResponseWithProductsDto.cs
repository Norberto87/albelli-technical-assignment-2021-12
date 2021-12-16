using System.Collections.Generic;

namespace TechnicalAssignment.Data.Models
{
    public class OrderResponseWithProductsDto : OrderResponseDto
    {
        public IEnumerable<OrderResponseProductDto> Products { get; set; }
    }
}
