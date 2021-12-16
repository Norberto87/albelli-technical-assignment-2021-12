using System.Collections.Generic;
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
    /// This class implements the products controller.
    /// It contains methods to get products from the database.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productsService">Service used in the controller.</param>
        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>Products retrieved from database.</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<ProductDto>))]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllAsync()
        {
            IEnumerable<ProductDto> products = await productsService.GetAllProductsAsync();

            return Ok(products);
        }

        /// <summary>
        /// Gets a single product.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns>Product retrieved from database. If the product does not exist, a <see cref="NotFoundResult"/> is returned instead.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProductDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ActionResult))]
        public async Task<ActionResult<ProductDto>> GetAsync(ProductType id)
        {
            var result = await productsService.GetProductAsync(id);

            return result.StatusCode == OperationStatusCode.Ok
                ? Ok(result.Data)
                : NotFound();
        }
    }
}
