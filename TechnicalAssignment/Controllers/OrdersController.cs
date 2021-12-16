using System.Net;
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

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(OrderResponseWithProductsDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ActionResult))]
        public async Task<ActionResult<OrderResponseWithProductsDto>> GetAsync(int id)
        {
            var result = await ordersService.GetOrderWithProductsAsync(id);

            return result.StatusCode == OperationStatusCode.Ok
                ? Ok(result.Data)
                : NotFound();
        }

        [HttpGet("{id}/status")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(OrderStatusDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ActionResult))]
        public async Task<ActionResult<OrderStatusDto>> GetStatusAsync(int id)
        {
            var result = await ordersService.GetOrderStatusAsync(id);

            return result.StatusCode == OperationStatusCode.Ok
                ? Ok(result.Data)
                : NotFound();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(OrderRequestWithProductsDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ActionResult))]
        public async Task<ActionResult<OrderResponseWithProductsDto>> PostAsync([FromBody] OrderRequestWithProductsDto order)
        {
            var result = await ordersService.CreateOrderAsync(order);

            return result.StatusCode == OperationStatusCode.Ok
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpPut("status")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(OrderStatusDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ActionResult))]
        public async Task<ActionResult> PutAsync([FromBody] OrderStatusDto order)
        {
            var result = await ordersService.UpdateOrderStatusAsync(order);

            return result.StatusCode == OperationStatusCode.Ok
                ? Ok()
                : NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ActionResult))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ActionResult))]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await ordersService.DeleteOrderAsync(id);

            return result.StatusCode == OperationStatusCode.Ok
                ? Ok()
                : NotFound();
        }
    }
}
