using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Services;
using TechnicalAssignment.Services.Models;

namespace TechnicalAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderRequestWithProductsDto>> GetAsync(int id)
        {
            OrderRequestDto order = await ordersService.GetOrderWithProductsAsync(id);

            return order != null
                ? Ok(order)
                : NotFound("No orders found for the order ID provided.");
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<OrderResponseWithProductsDto>> Post([FromBody] OrderRequestWithProductsDto order)
        {
            var result = await ordersService.CreateOrderAsync(order);

            return result.StatusCode == OperationStatusCode.Ok
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
