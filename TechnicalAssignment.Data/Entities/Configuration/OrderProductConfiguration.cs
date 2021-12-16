using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Entities.Configuration
{
    internal class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("OrderProduct").HasKey(op => new { op.OrderId, op.ProductId });

            builder.HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new OrderProduct
                {
                    OrderId = 1,
                    ProductId = ProductType.Canvas,
                    Quantity = 2,
                },

                new OrderProduct
                {
                    OrderId = 2,
                    ProductId = ProductType.PhotoBook,
                    Quantity = 1,
                },

                new OrderProduct
                {
                    OrderId = 2,
                    ProductId = ProductType.Cards,
                    Quantity = 3,
                },

                new OrderProduct
                {
                    OrderId = 3,
                    ProductId = ProductType.Mug,
                    Quantity = 2,
                },

                new OrderProduct
                {
                    OrderId = 3,
                    ProductId = ProductType.Calendar,
                    Quantity = 4,
                });
        }
    }
}
