using System;
using System.Collections.Generic;

using Moq;

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

        private HashSet<int> GetValidProductTypes()
        {
            HashSet<int> productTypes = new HashSet<int>();
            productTypes.Add(1);
            productTypes.Add(2);
            productTypes.Add(3);

            return productTypes;
        }
    }
}
