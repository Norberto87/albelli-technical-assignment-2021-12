using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Services.Models;
using TechnicalAssignment.Services.Tests.Mocks;

namespace TechnicalAssignment.Services.Tests
{
    [TestClass]
    public class OrdersServiceTests
    {
        private UnitOfWorkMock unitOfWorkMock;
        private OrdersService service;

        [TestInitialize]
        public void Setup()
        {
            unitOfWorkMock = new UnitOfWorkMock();
            service = new OrdersService(unitOfWorkMock.Object);

            unitOfWorkMock.Setup(u => u.OrdersRepository).Returns(unitOfWorkMock.OrdersRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.ProductsRepository).Returns(unitOfWorkMock.ProductsRepositoryMock.Object);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnInvalidDataStatusCode_WhenOrderIsNull()
        {
            var result = await service.CreateOrderAsync(null);

            Assert.AreEqual(OperationStatusCode.InvalidData, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnInvalidDataStatusCode_WhenIdIsNotValid()
        {
            var result = await service.CreateOrderAsync(new OrderWithProductsDto { Id = -1 });

            Assert.AreEqual(OperationStatusCode.InvalidData, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnInvalidDataStatusCode_WhenProductsListIsNull()
        {
            var result = await service.CreateOrderAsync(new OrderWithProductsDto { Id = 1 });

            Assert.AreEqual(OperationStatusCode.InvalidData, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnInvalidDataStatusCode_WhenProductIdIsNotValid()
        {
            var products = new List<OrderProductDto>();
            products.Add(new OrderProductDto { Id = 0, Quantity = 1 });

            var result = await service.CreateOrderAsync(new OrderWithProductsDto { Id = 1, Products = products });

            Assert.AreEqual(OperationStatusCode.InvalidData, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnInvalidDataStatusCode_WhenProductQuantityIsNotValid()
        {
            var products = new List<OrderProductDto>();
            products.Add(new OrderProductDto { Id = 1, Quantity = 0 });

            var result = await service.CreateOrderAsync(new OrderWithProductsDto { Id = 1, Products = products });

            Assert.AreEqual(OperationStatusCode.InvalidData, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnInvalidDataStatusCode_WhenProductsListIsEmpty()
        {
            var result = await service.CreateOrderAsync(new OrderWithProductsDto { Id = 1, Products = new List<OrderProductDto>() });

            Assert.AreEqual(OperationStatusCode.InvalidData, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnAlreadyExistsStatusCode_WhenOrderIdAlreadyExists()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync(new OrderWithProductsDto());

            var orderWithProducts = CreateValidOrderWithProducts();

            var result = await service.CreateOrderAsync(orderWithProducts);

            Assert.AreEqual(OperationStatusCode.AlreadyExists, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnOkStatusCode_WhenProductsListIsEmpty()
        {
            var products = new List<OrderProductDto>();
            products.Add(new OrderProductDto { Id = 1, Quantity = 1 });

            var result = await service.CreateOrderAsync(new OrderWithProductsDto { Id = 1, Products = products });

            Assert.AreEqual(OperationStatusCode.Ok, result.StatusCode);
        }

        private OrderWithProductsDto CreateValidOrderWithProducts()
        {
            var products = new List<OrderProductDto>();
            products.Add(new OrderProductDto { Id = 1, Quantity = 1 });
            products.Add(new OrderProductDto { Id = 2, Quantity = 2 });
            products.Add(new OrderProductDto { Id = 3, Quantity = 3 });

            return new OrderWithProductsDto { Id = 1, Products = products };
        }
    }
}