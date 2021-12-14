using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TechnicalAssignment.Data.Entities;
using TechnicalAssignment.Data.Entities.Configuration;

namespace TechnicalAssignment.Data.Persistence
{
    public class TechnicalAssignmentDbContext : DbContext, ITechnicalAssignmentDbContext
    {
        public TechnicalAssignmentDbContext(DbContextOptions<TechnicalAssignmentDbContext> options)
            : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        // TODO: remove method.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
        }
    }
}
