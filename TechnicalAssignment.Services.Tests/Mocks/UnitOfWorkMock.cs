using System.Threading.Tasks;

using Moq;

using TechnicalAssignment.Data.Persistence;
using TechnicalAssignment.Data.Persistence.Repositories;

namespace TechnicalAssignment.Services.Tests.Mocks
{
    internal class UnitOfWorkMock : IUnitOfWork
    {
        public UnitOfWorkMock()
        {
            OrdersRepositoryMock = new OrdersRepositoryMock();
        }

        public OrdersRepositoryMock OrdersRepositoryMock;

        public IOrdersRepository OrdersRepository => OrdersRepositoryMock.Object;

        public IProductsRepository ProductsRepository => throw new System.NotImplementedException();

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
