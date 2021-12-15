using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TechnicalAssignment.Data.Models.Enums;

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
                    Id = 1,
                    Status = (int)OrderStatusType.Received
                },

                new Order
                {
                    Id = 2,
                    Status = (int)OrderStatusType.Processing
                },

                new Order
                {
                    Id = 3,
                    Status = (int)OrderStatusType.Shipped
                });
        }
    }
}
