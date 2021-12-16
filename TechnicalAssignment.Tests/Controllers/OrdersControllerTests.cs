using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using TechnicalAssignment.Controllers;
using TechnicalAssignment.Data.Models;
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
            service.Setup(s => s.GetOrderWithProductsAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            ActionResult<OrderRequestWithProductsDto> actionResult = await controller.GetAsync(1);

            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);

            var httpResult = actionResult.Result as NotFoundObjectResult;

            Assert.IsNotNull(httpResult);
            Assert.AreEqual((int)HttpStatusCode.NotFound, httpResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnOrder_WhenOrderIdExists()
        {
            service.Setup(s => s.GetOrderWithProductsAsync(It.IsAny<int>())).ReturnsAsync(() => new OrderRequestWithProductsDto());

            ActionResult<OrderRequestWithProductsDto> actionResult = await controller.GetAsync(1);

            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);

            var httpResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(httpResult);
            Assert.AreEqual((int)HttpStatusCode.OK, httpResult.StatusCode);
        }
    }
}
