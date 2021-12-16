using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TechnicalAssignment.Data.Entities;
using TechnicalAssignment.Data.Entities.Configuration;

namespace TechnicalAssignment.Data.Persistence
{
    /// <summary>
    /// Implements a DB context.
    /// </summary>
    public class TechnicalAssignmentDbContext : DbContext, ITechnicalAssignmentDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicalAssignmentDbContext"/> class.
        /// </summary>
        /// <param name="options">DB context options.</param>
        public TechnicalAssignmentDbContext(DbContextOptions<TechnicalAssignmentDbContext> options)
            : base(options)
        {

        }

        /// <inheritdoc/>
        public DbSet<Order> Orders { get; set; }

        /// <inheritdoc/>
        public DbSet<Product> Products { get; set; }

        /// <inheritdoc/>
        public DbSet<OrderProduct> OrderProducts { get; set; }

        /// <inheritdoc/>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            if (!ChangeTracker.AutoDetectChangesEnabled)
            {
                ChangeTracker.AutoDetectChangesEnabled = true;
            }

            return result;
        }

        /// <inheritdoc/>
        public void SetBulkMode(bool bulkEnabled)
        {
            ChangeTracker.AutoDetectChangesEnabled = bulkEnabled;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Uncomment to debug database operations.
            //optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
        }
    }
}
