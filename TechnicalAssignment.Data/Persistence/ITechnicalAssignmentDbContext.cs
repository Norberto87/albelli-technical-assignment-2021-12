using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using TechnicalAssignment.Data.Entities;

namespace TechnicalAssignment.Data.Persistence
{
    /// <summary>
    /// Provides the interface to be implemented by a DB context.
    /// </summary>
    public interface ITechnicalAssignmentDbContext : IDisposable
    {
        /// <summary>
        /// Gets or sets the orders DB set.
        /// </summary>
        DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the products DB set.
        /// </summary>
        DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the order products DB set.
        /// </summary>
        DbSet<OrderProduct> OrderProducts { get; set; }

        /// <summary>
        /// Saves the changes in the current context.
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets the properties to disable or enable bulk operations.
        /// </summary>
        /// <param name="bulkEnabled">True if bulk mode is to be enabled, and false otherwise.</param>
        void SetBulkMode(bool bulkEnabled);

        /// <summary>
        /// Gets the entry for the entity.
        /// </summary>
        /// <param name="entity">Entity data.</param>
        /// <returns>Entity entry.</returns>
        EntityEntry Entry(object entity);
    }
}
