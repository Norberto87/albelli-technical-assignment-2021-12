using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechnicalAssignment.Data.Entities.Configuration
{
    internal class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("OrderProduct").HasKey(op => new { op.OrderId, op.ProductId });

            builder.HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            builder.HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);

            builder.HasData(
                new OrderProduct
                {
                    OrderId = 1,
                    ProductId = 3,
                    Quantity = 2,
                },

                new OrderProduct
                {
                    OrderId = 2,
                    ProductId = 1,
                    Quantity = 1,
                },

                new OrderProduct
                {
                    OrderId = 2,
                    ProductId = 4,
                    Quantity = 3,
                },

                new OrderProduct
                {
                    OrderId = 3,
                    ProductId = 5,
                    Quantity = 2,
                },

                new OrderProduct
                {
                    OrderId = 3,
                    ProductId = 2,
                    Quantity = 4,
                });
        }
    }
}
