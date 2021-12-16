using System.Collections.Generic;
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
            var result = await service.CreateOrderAsync(new OrderRequestWithProductsDto { OrderId = -1 });

            Assert.AreEqual(OperationStatusCode.InvalidData, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnInvalidDataStatusCode_WhenProductsListIsNull()
        {
            var result = await service.CreateOrderAsync(new OrderRequestWithProductsDto { OrderId = 1 });

            Assert.AreEqual(OperationStatusCode.InvalidData, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnInvalidDataStatusCode_WhenProductIdIsNotValid()
        {
            var products = new List<OrderRequestProductDto>();
            products.Add(new OrderRequestProductDto { ProductType = 0, Quantity = 1 });

            var result = await service.CreateOrderAsync(new OrderRequestWithProductsDto { OrderId = 1, Products = products });

            Assert.AreEqual(OperationStatusCode.InvalidData, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnInvalidDataStatusCode_WhenProductQuantityIsNotValid()
        {
            var products = new List<OrderRequestProductDto>();
            products.Add(new OrderRequestProductDto { ProductType = ProductType.PhotoBook, Quantity = 0 });

            var result = await service.CreateOrderAsync(new OrderRequestWithProductsDto { OrderId = 1, Products = products });

            Assert.AreEqual(OperationStatusCode.InvalidData, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnInvalidDataStatusCode_WhenProductsListIsEmpty()
        {
            var result = await service.CreateOrderAsync(new OrderRequestWithProductsDto { OrderId = 1, Products = new List<OrderRequestProductDto>() });

            Assert.AreEqual(OperationStatusCode.InvalidData, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnAlreadyExistsStatusCode_WhenOrderIdAlreadyExists()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync(new OrderRequestWithProductsDto());

            var orderWithProducts = CreateValidOrderRequestWithProducts();

            var result = await service.CreateOrderAsync(orderWithProducts);

            Assert.AreEqual(OperationStatusCode.AlreadyExists, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnOkStatusCode_WhenOrderIsValid()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<OrderRequestWithProductsDto>())).ReturnsAsync(new OrderResponseWithProductsDto { OrderId = 1 });
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetOrderProductsAsync(It.IsAny<int>())).ReturnsAsync(CreateValidOrderProducts());

            var result = await service.CreateOrderAsync(CreateValidOrderRequestWithProducts());

            Assert.AreEqual(OperationStatusCode.Ok, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnValidMinimumRequiredWidth_WhenStackSizeIsOne()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<OrderRequestWithProductsDto>())).ReturnsAsync(new OrderResponseWithProductsDto { OrderId = 1 });
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetOrderProductsAsync(It.IsAny<int>())).ReturnsAsync(CreateValidOrderResponseSingleProduct(10, 1, 12.5f));

            var result = await service.CreateOrderAsync(CreateValidOrderRequestWithSingleProduct(1));

            Assert.AreEqual(125f, result.Data.RequiredBinWidth);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnValidMinimumRequiredWidth_WhenQuantityIsOneAndStackSizeIsFive()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<OrderRequestWithProductsDto>())).ReturnsAsync(new OrderResponseWithProductsDto { OrderId = 1 });
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetOrderProductsAsync(It.IsAny<int>())).ReturnsAsync(CreateValidOrderResponseSingleProduct(1, 5, 12.5f));

            var result = await service.CreateOrderAsync(CreateValidOrderRequestWithSingleProduct(1));

            Assert.AreEqual(12.5f, result.Data.RequiredBinWidth);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnValidMinimumRequiredWidth_WhenQuantityIsFiveAndStackSizeIsOne()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<OrderRequestWithProductsDto>())).ReturnsAsync(new OrderResponseWithProductsDto { OrderId = 1 });
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetOrderProductsAsync(It.IsAny<int>())).ReturnsAsync(CreateValidOrderResponseSingleProduct(5, 1, 12.5f));

            var result = await service.CreateOrderAsync(CreateValidOrderRequestWithSingleProduct(1));

            Assert.AreEqual(62.5f, result.Data.RequiredBinWidth);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnValidMinimumRequiredWidth_WhenQuantityIsFourAndStackSizeIsFive()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<OrderRequestWithProductsDto>())).ReturnsAsync(new OrderResponseWithProductsDto { OrderId = 1 });
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetOrderProductsAsync(It.IsAny<int>())).ReturnsAsync(CreateValidOrderResponseSingleProduct(4, 5, 12.5f));

            var result = await service.CreateOrderAsync(CreateValidOrderRequestWithSingleProduct(1));

            Assert.AreEqual(12.5f, result.Data.RequiredBinWidth);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldReturnValidMinimumRequiredWidth_WhenQuantityIsSixAndStackSizeIsFive()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<OrderRequestWithProductsDto>())).ReturnsAsync(new OrderResponseWithProductsDto { OrderId = 1 });
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetOrderProductsAsync(It.IsAny<int>())).ReturnsAsync(CreateValidOrderResponseSingleProduct(6, 5, 12.5f));

            var result = await service.CreateOrderAsync(CreateValidOrderRequestWithSingleProduct(1));

            Assert.AreEqual(25f, result.Data.RequiredBinWidth);
        }

        [TestMethod]
        public async Task GetOrderWithProductsAsync_ShouldReturnNotFoundStatusCode_WhenOrderDoesNotExist()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetOrderWithProductsAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            var result = await service.GetOrderWithProductsAsync(1);

            Assert.AreEqual(OperationStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public async Task GetOrderWithProductsAsync_ShouldReturnOkStatusCode_WhenOrderDoesNotExist()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetOrderWithProductsAsync(It.IsAny<int>())).ReturnsAsync(() => new OrderResponseWithProductsDto());

            var result = await service.GetOrderWithProductsAsync(1);

            Assert.AreEqual(OperationStatusCode.Ok, result.StatusCode);
        }

        [TestMethod]
        public async Task GetOrderStatusAsync_ShouldReturnNotFoundStatusCode_WhenOrderDoesNotExist()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetStatusAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            var result = await service.GetOrderStatusAsync(1);

            Assert.AreEqual(OperationStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public async Task GetOrderStatusAsync_ShouldReturnOkStatusCode_WhenOrderDoesNotExist()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetStatusAsync(It.IsAny<int>())).ReturnsAsync(() => new OrderStatusDto());

            var result = await service.GetOrderStatusAsync(1);

            Assert.AreEqual(OperationStatusCode.Ok, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteOrderAsync_ShouldReturnNotFoundStatusCode_WhenOrderDoesNotExist()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            var result = await service.DeleteOrderAsync(1);

            Assert.AreEqual(OperationStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteOrderAsync_ShouldReturnOkStatusCode_WhenOrderExists()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new OrderRequestDto());

            var result = await service.DeleteOrderAsync(1);

            Assert.AreEqual(OperationStatusCode.Ok, result.StatusCode);
        }

        [TestMethod]
        public async Task UpdateOrderStatusAsync_ShouldReturnNotFoundStatusCode_WhenOrderDoesNotExist()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            var result = await service.UpdateOrderStatusAsync(new OrderStatusDto());

            Assert.AreEqual(OperationStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public async Task UpdateOrderStatusAsync_ShouldReturnOkStatusCode_WhenOrderExists()
        {
            unitOfWorkMock.OrdersRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync(() => new OrderRequestDto());

            var result = await service.UpdateOrderStatusAsync(new OrderStatusDto());

            Assert.AreEqual(OperationStatusCode.Ok, result.StatusCode);
        }

        private OrderRequestWithProductsDto CreateValidOrderRequestWithProducts()
        {
            var products = new List<OrderRequestProductDto>();
            products.Add(new OrderRequestProductDto { ProductType = ProductType.PhotoBook, Quantity = 1 });
            products.Add(new OrderRequestProductDto { ProductType = ProductType.Calendar, Quantity = 2 });
            products.Add(new OrderRequestProductDto { ProductType = ProductType.Canvas, Quantity = 3 });

            return new OrderRequestWithProductsDto { OrderId = 1, Products = products };
        }

        private OrderRequestWithProductsDto CreateValidOrderRequestWithSingleProduct(int quantity)
        {
            var products = new List<OrderRequestProductDto>();
            products.Add(new OrderRequestProductDto { ProductType = ProductType.PhotoBook, Quantity = quantity });

            return new OrderRequestWithProductsDto { OrderId = 1, Products = products };
        }

        private IEnumerable<OrderProductDto> CreateValidOrderProducts()
        {
            var products = new List<OrderProductDto>();
            products.Add(new OrderProductDto { ProductType = ProductType.PhotoBook, Quantity = 1, StackSize = 1 });
            products.Add(new OrderProductDto { ProductType = ProductType.Calendar, Quantity = 2, StackSize = 2 });
            products.Add(new OrderProductDto { ProductType = ProductType.Canvas, Quantity = 3, StackSize = 3 });

            return products;
        }

        private IEnumerable<OrderProductDto> CreateValidOrderResponseSingleProduct(int quantity, int stackSize, float width)
        {
            var products = new List<OrderProductDto>();
            products.Add(new OrderProductDto { ProductType = ProductType.PhotoBook, Quantity = quantity, StackSize = stackSize, Width = width });

            return products;
        }
    }
}