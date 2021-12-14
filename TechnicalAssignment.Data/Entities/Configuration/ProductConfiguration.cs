using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TechnicalAssignment.Data.Models.Enums;

namespace TechnicalAssignment.Data.Entities.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product").HasKey(p => p.Id);

            builder.Property(p => p.Name).HasMaxLength(25).IsRequired();

            builder.Property(p => p.Width).IsRequired();

            builder.HasData(
                new Product
                {
                    Id = (int)ProductType.Calendar,
                    Name = nameof(ProductType.Calendar),
                    Width = 10
                },

                new Product
                {
                    Id = (int)ProductType.Canvas,
                    Name = nameof(ProductType.Canvas),
                    Width = 16
                },

                new Product
                {
                    Id = (int)ProductType.Cards,
                    Name = nameof(ProductType.Cards),
                    Width = 4.7f
                },

                new Product
                {
                    Id = (int)ProductType.Mug,
                    Name = nameof(ProductType.Mug),
                    Width = 94
                },

                new Product
                {
                    Id = (int)ProductType.PhotoBook,
                    Name = nameof(ProductType.PhotoBook),
                    Width = 19
                });
        }
    }
}
