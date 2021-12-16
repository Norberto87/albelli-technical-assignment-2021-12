using System;
using System.Threading.Tasks;
using TechnicalAssignment.Data.Persistence.Repositories;

namespace TechnicalAssignment.Data.Persistence
{
    /// <summary>
    /// Provides the interface to be implemented by a unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the orders repository.
        /// </summary>
        IOrdersRepository OrdersRepository { get; }

        /// <summary>
        /// Gets the products repository.
        /// </summary>
        IProductsRepository ProductsRepository { get; }

        /// <summary>
        /// Saves the pending changes in the DB context.
        /// </summary>
        /// <returns>Number of entities updated.</returns>
        Task<int> SaveAsync();
    }
}
