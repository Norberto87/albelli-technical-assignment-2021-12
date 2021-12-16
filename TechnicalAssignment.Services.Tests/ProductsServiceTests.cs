using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Data.Models.Enums;
using TechnicalAssignment.Services.Models;
using TechnicalAssignment.Services.Tests.Mocks;

namespace TechnicalAssignment.Services.Tests
{
    [TestClass]
    public class ProductsServiceTests
    {
        private UnitOfWorkMock unitOfWorkMock;
        private ProductsService service;

        [TestInitialize]
        public void Setup()
        {
            unitOfWorkMock = new UnitOfWorkMock();
            service = new ProductsService(unitOfWorkMock.Object);

            unitOfWorkMock.Setup(u => u.ProductsRepository).Returns(unitOfWorkMock.ProductsRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetProductAsync_ShouldReturnNotFoundStatusCode_WhenProductDoesNotExist()
        {
            unitOfWorkMock.ProductsRepositoryMock.Setup(r => r.GetAsync(It.IsAny<ProductType>())).ReturnsAsync(() => null);
            var result = await service.GetProductAsync(ProductType.PhotoBook);

            Assert.IsNotNull(result);
            Assert.AreEqual(OperationStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public async Task GetProductAsync_ShouldReturnValidProduct_WhenProductIdExists()
        {
            unitOfWorkMock.ProductsRepositoryMock.Setup(r => r.GetAsync(It.IsAny<ProductType>())).ReturnsAsync(() => new ProductDto());
            var result = await service.GetProductAsync(ProductType.PhotoBook);

            Assert.IsNotNull(result);
            Assert.AreEqual(OperationStatusCode.Ok, result.StatusCode);
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ShouldReturnEmptyCollection_WhenNoProductsExist()
        {
            unitOfWorkMock.ProductsRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(() => new List<ProductDto>());
            var result = await service.GetAllProductsAsync();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts_WhenProductsExist()
        {
            unitOfWorkMock.ProductsRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(() => GetValidProducts());
            var result = await service.GetAllProductsAsync();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
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
