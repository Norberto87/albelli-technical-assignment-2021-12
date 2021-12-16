using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using TechnicalAssignment.Controllers;
using TechnicalAssignment.Data.Models;
using TechnicalAssignment.Services.Models;
using TechnicalAssignment.Tests.Mocks;

namespace TechnicalAssignment.Tests.Controllers
{
    [TestClass]
    public class OrdersControllerTests
    {
        private OrdersServiceMock service;
        private OrdersController controller;

        [TestInitialize]
        public void Setup()
        {
            service = new OrdersServiceMock();
            controller = new OrdersController(service.Object);
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnStatusNotFound_WhenOrderIdDoesNotExist()
        {
            service.Setup(s => s.GetOrderWithProductsAsync(It.IsAny<int>())).ReturnsAsync(() => new OperationResultWithData<OrderResponseWithProductsDto> { StatusCode = OperationStatusCode.NotFound });

            var result = await controller.GetAsync(1);

            Assert.IsNotNull(result);

            var httpResult = result.Result as NotFoundResult;

            Assert.IsNotNull(httpResult);
            Assert.AreEqual((int)HttpStatusCode.NotFound, httpResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnOrder_WhenOrderIdExists()
        {
            int orderId = 1;
            service.Setup(s => s.GetOrderWithProductsAsync(It.IsAny<int>())).ReturnsAsync(() => new OperationResultWithData<OrderResponseWithProductsDto>() { Data = new OrderResponseWithProductsDto { OrderId = orderId } });

            var result = await controller.GetAsync(orderId);

            Assert.IsNotNull(result);

            var httpResult = result.Result as OkObjectResult;

            Assert.IsNotNull(httpResult);
            Assert.AreEqual((int)HttpStatusCode.OK, httpResult.StatusCode);

            var resultData = httpResult.Value as OrderResponseWithProductsDto;

            Assert.IsNotNull(resultData);
            Assert.AreEqual(orderId, resultData.OrderId);
        }

        [TestMethod]
        public async Task GetOrderStatusAsync_ShouldReturnStatusNotFound_WhenOrderDoesNotExist()
        {
            int orderId = 1;
            service.Setup(s => s.GetOrderStatusAsync(It.IsAny<int>())).ReturnsAsync(() => new OperationResultWithData<OrderStatusDto>() { StatusCode = OperationStatusCode.NotFound });

            var result = await controller.GetStatusAsync(orderId);

            Assert.IsNotNull(result);

            var httpResult = result.Result as NotFoundResult;

            Assert.IsNotNull(httpResult);
            Assert.AreEqual((int)HttpStatusCode.NotFound, httpResult.StatusCode);
        }

        [TestMethod]
        public async Task GetOrderStatusAsync_ShouldReturnOrderStatus_WhenOrderIdExists()
        {
            int orderId = 1;
            service.Setup(s => s.GetOrderStatusAsync(It.IsAny<int>())).ReturnsAsync(() => new OperationResultWithData<OrderStatusDto>() { Data = new OrderStatusDto { OrderId = orderId } });

            var result = await controller.GetStatusAsync(orderId);

            Assert.IsNotNull(result);

            var httpResult = result.Result as OkObjectResult;

            Assert.IsNotNull(httpResult);
            Assert.AreEqual((int)HttpStatusCode.OK, httpResult.StatusCode);

            var resultData = httpResult.Value as OrderStatusDto;

            Assert.IsNotNull(resultData);
            Assert.AreEqual(orderId, resultData.OrderId);
        }

        [TestMethod]
        public async Task Post_ShouldReturnStatusBadRequest_WhenOrderDataIsNotValid()
        {
            service.Setup(s => s.CreateOrderAsync(It.IsAny<OrderRequestWithProductsDto>())).ReturnsAsync(() => new OperationResultWithData<OrderResponseWithProductsDto> { StatusCode = OperationStatusCode.InvalidData });

            var result = await controller.PostAsync(new OrderRequestWithProductsDto());

            Assert.IsNotNull(result);

            var httpResult = result.Result as BadRequestObjectResult;

            Assert.IsNotNull(httpResult);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, httpResult.StatusCode);
        }

        [TestMethod]
        public async Task Post_ShouldReturnCreatedOrder_WhenOrderDataIsValid()
        {
            int orderId = 1;
            service.Setup(s => s.CreateOrderAsync(It.IsAny<OrderRequestWithProductsDto>())).ReturnsAsync(() => new OperationResultWithData<OrderResponseWithProductsDto>(OperationStatusCode.Ok, new OrderResponseWithProductsDto { OrderId = orderId }));

            var result = await controller.PostAsync(new OrderRequestWithProductsDto());

            Assert.IsNotNull(result);

            var httpResult = result.Result as OkObjectResult;

            Assert.IsNotNull(httpResult);
            Assert.AreEqual((int)HttpStatusCode.OK, httpResult.StatusCode);

            var resultData = httpResult.Value as OrderResponseWithProductsDto;

            Assert.IsNotNull(resultData);
            Assert.AreEqual(orderId, resultData.OrderId);
        }

        [TestMethod]
        public async Task Put_ShouldReturnStatusNotFound_WhenOrderDataIsNotValid()
        {
            service.Setup(s => s.UpdateOrderStatusAsync(It.IsAny<OrderStatusDto>())).ReturnsAsync(() => new OperationResult(OperationStatusCode.NotFound));

            var result = await controller.PutAsync(new OrderStatusDto());

            Assert.IsNotNull(result);

            var httpResult = result as NotFoundResult;

            Assert.IsNotNull(httpResult);
            Assert.AreEqual((int)HttpStatusCode.NotFound, httpResult.StatusCode);
        }

        [TestMethod]
        public async Task Put_ShouldReturnStatusOk_WhenOrderDataIsValid()
        {
            service.Setup(s => s.UpdateOrderStatusAsync(It.IsAny<OrderStatusDto>())).ReturnsAsync(() => new OperationResult(OperationStatusCode.Ok));

            var result = await controller.PutAsync(new OrderStatusDto());

            Assert.IsNotNull(result);

            var httpResult = result as OkResult;

            Assert.IsNotNull(httpResult);
            Assert.AreEqual((int)HttpStatusCode.OK, httpResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnStatusNotFound_WhenOrderDataIsNotValid()
        {
            service.Setup(s => s.DeleteOrderAsync(It.IsAny<int>())).ReturnsAsync(() => new OperationResult(OperationStatusCode.NotFound));

            var result = await controller.DeleteAsync(1);

            Assert.IsNotNull(result);

            var httpResult = result as NotFoundResult;

            Assert.IsNotNull(httpResult);
            Assert.AreEqual((int)HttpStatusCode.NotFound, httpResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnStatusOk_WhenOrderDataIsValid()
        {
            service.Setup(s => s.DeleteOrderAsync(It.IsAny<int>())).ReturnsAsync(() => new OperationResult(OperationStatusCode.Ok));

            var result = await controller.DeleteAsync(1);

            Assert.IsNotNull(result);

            var httpResult = result as OkResult;

            Assert.IsNotNull(httpResult);
            Assert.AreEqual((int)HttpStatusCode.OK, httpResult.StatusCode);
        }
    }
}
