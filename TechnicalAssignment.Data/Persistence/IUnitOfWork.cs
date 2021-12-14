using System;
using System.Threading.Tasks;
using TechnicalAssignment.Data.Persistence.Repositories;

namespace TechnicalAssignment.Data.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IOrdersRepository OrdersRepository { get; }

        IProductsRepository ProductsRepository { get; }

        Task<int> SaveAsync();
    }
}
