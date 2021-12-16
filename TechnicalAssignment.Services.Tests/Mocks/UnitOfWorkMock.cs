using System;
using System.Collections.Generic;

using Moq;

using TechnicalAssignment.Data.Models.Enums;
using TechnicalAssignment.Data.Persistence;

namespace TechnicalAssignment.Services.Tests.Mocks
{
    internal class UnitOfWorkMock : Mock<IUnitOfWork>
    {
        public UnitOfWorkMock()
        {
            OrdersRepositoryMock = new OrdersRepositoryMock();
            ProductsRepositoryMock = new ProductsRepositoryMock();

            ConfigureRepositoriesDefaultBehaviour();
        }

        public OrdersRepositoryMock OrdersRepositoryMock { get; set; }

        public ProductsRepositoryMock ProductsRepositoryMock { get; set; }

        private void ConfigureRepositoriesDefaultBehaviour()
        {
            ProductsRepositoryMock.Setup(p => p.GetAllProductTypesAsync()).ReturnsAsync(GetValidProductTypes());
        }

        private HashSet<ProductType> GetValidProductTypes()
        {
            HashSet<ProductType> productTypes = new HashSet<ProductType>();
            productTypes.Add(ProductType.PhotoBook);
            productTypes.Add(ProductType.Calendar);
            productTypes.Add(ProductType.Canvas);

            return productTypes;
        }
    }
}
