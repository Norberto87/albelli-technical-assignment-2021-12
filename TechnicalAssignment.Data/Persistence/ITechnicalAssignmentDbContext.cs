using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TechnicalAssignment.Data.Entities;

namespace TechnicalAssignment.Data.Persistence
{
    public interface ITechnicalAssignmentDbContext : IDisposable
    {
        DbSet<Order> Orders { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<OrderProduct> OrderProducts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
