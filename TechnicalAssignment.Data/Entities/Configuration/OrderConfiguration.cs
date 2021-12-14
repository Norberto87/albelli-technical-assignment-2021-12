using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechnicalAssignment.Data.Entities.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order").HasKey(o => o.Id);

            builder.HasData(
                new Order
                {
                    Id = 1
                },

                new Order
                {
                    Id = 2
                },

                new Order
                {
                    Id = 3
                });
        }
    }
}
