using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Data.Models.Enums;
using TechnicalAssignment.Services;
using TechnicalAssignment.Services.Models;

namespace TechnicalAssignment.Controllers
{
    /// <summary>
    /// This class implements the orders controller.
    /// It contains methods to manage orders, by adding, deleting, reading and updating.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService ordersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="ordersService">Service used in the controller.</param>
        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        /// <summary>
        /// Gets a single order.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <returns>Order retrieved from database. If the order does not exist, a <see cref="NotFoundResult"/> is returned instead.</returns>
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

        /// <summary>
        /// Gets the status of an order.
        /// </summary>
        /// <param name="id">Order ID to be retrieved.</param>
        /// <returns>Order status retrieved from database. If the order does not exist, a <see cref="NotFoundResult"/> is returned instead.</returns>
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

        /// <summary>
        /// Adds a new order.
        /// </summary>
        /// <param name="order">Order to be created.</param>
        /// <returns>Created order, including all products with corresponding widths and minimum required bin width. If the order is not created, a <see cref="BadRequestObjectResult"/> is returned instead.</returns>
        /// <remarks>The order status is set to <see cref="OrderStatusType.Received"/>.</remarks>
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

        /// <summary>
        /// Updates the status of an order.
        /// </summary>
        /// <param name="order">Order to be updated.</param>
        /// <returns>HTTP OK result. If the order does not exist, a <see cref="NotFoundResult"/> is returned instead.</returns>
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

        /// <summary>
        /// Deletes an order.
        /// </summary>
        /// <param name="id">Order ID to be deleted.</param>
        /// <returns>HTTP OK result. If the order does not exist, a <see cref="NotFoundResult"/> is returned instead.</returns>
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
