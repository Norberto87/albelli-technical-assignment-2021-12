using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using TechnicalAssignment.Controllers;
using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Data.Models.Enums;
using TechnicalAssignment.Services.Models;
using TechnicalAssignment.Tests.Mocks;

namespace TechnicalAssignment.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTests
    {
        private ProductsServiceMock service;
        private ProductsController controller;

        [TestInitialize]
        public void Setup()
        {
            service = new ProductsServiceMock();
            controller = new ProductsController(service.Object);
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNotFoundStatusCode_WhenProductIdDoesNotExist()
        {
            service.Setup(s => s.GetProductAsync(It.IsAny<ProductType>())).ReturnsAsync(new OperationResultWithData<ProductDto> { StatusCode = OperationStatusCode.NotFound });

            var result = await controller.GetAsync(ProductType.PhotoBook);

            Assert.IsNotNull(result);

            var httpResult = result.Result as NotFoundResult;

            Assert.IsNotNull(httpResult);
            Assert.AreEqual((int)HttpStatusCode.NotFound, httpResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnOkStatusCode_WhenProductIdExists()
        {
            service.Setup(s => s.GetProductAsync(It.IsAny<ProductType>())).ReturnsAsync(new OperationResultWithData<ProductDto> { StatusCode = OperationStatusCode.Ok, Data = new ProductDto() });

            var result = await controller.GetAsync(ProductType.PhotoBook);

            Assert.IsNotNull(result);

            var httpResult = result.Result as OkObjectResult;

            Assert.IsNotNull(httpResult);
            Assert.AreEqual((int)HttpStatusCode.OK, httpResult.StatusCode);

            var value = httpResult.Value as ProductDto;

            Assert.IsNotNull(value);
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnEmptyCollection_WhenNoProductsExist()
        {
            service.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(new List<ProductDto>());

            var result = await controller.GetAllAsync();

            Assert.IsNotNull(result);

            var httpResult = result.Result as OkObjectResult;

            Assert.IsNotNull(httpResult);

            var value = httpResult.Value as IEnumerable<ProductDto>;

            Assert.IsNotNull(value);
            Assert.IsFalse(value.Any());
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllProducts_WhenProductsExist()
        {
            service.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(GetValidProducts());

            var result = await controller.GetAllAsync();

            Assert.IsNotNull(result);

            var httpResult = result.Result as OkObjectResult;

            Assert.IsNotNull(httpResult);

            var value = httpResult.Value as IEnumerable<ProductDto>;

            Assert.IsNotNull(value);
            Assert.IsTrue(value.Any());
        }

        private IEnumerable<ProductDto> GetValidProducts()
        {
            var products = new List<ProductDto>();
            products.Add(new ProductDto { Name = ProductType.PhotoBook.ToString(), ProductType = ProductType.PhotoBook });
            products.Add(new ProductDto { Name = ProductType.Mug.ToString(), ProductType = ProductType.Mug });
            products.Add(new ProductDto { Name = ProductType.Calendar.ToString(), ProductType = ProductType.Calendar });

            return products;
        }
    }
}
