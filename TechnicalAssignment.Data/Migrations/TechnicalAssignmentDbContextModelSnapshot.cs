﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechnicalAssignment.Data.Persistence;

#nullable disable

namespace TechnicalAssignment.Data.Migrations
{
    [DbContext(typeof(TechnicalAssignmentDbContext))]
    partial class TechnicalAssignmentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TechnicalAssignment.Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("Order", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1
                        },
                        new
                        {
                            Id = 2
                        },
                        new
                        {
                            Id = 3
                        });
                });

            modelBuilder.Entity("TechnicalAssignment.Data.Entities.OrderProduct", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProduct", (string)null);

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            ProductId = 3,
                            Quantity = 2
                        },
                        new
                        {
                            OrderId = 2,
                            ProductId = 1,
                            Quantity = 1
                        },
                        new
                        {
                            OrderId = 2,
                            ProductId = 4,
                            Quantity = 3
                        },
                        new
                        {
                            OrderId = 3,
                            ProductId = 5,
                            Quantity = 2
                        },
                        new
                        {
                            OrderId = 3,
                            ProductId = 2,
                            Quantity = 4
                        });
                });

            modelBuilder.Entity("TechnicalAssignment.Data.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Product", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Name = "Calendar",
                            Width = 10f
                        },
                        new
                        {
                            Id = 3,
                            Name = "Canvas",
                            Width = 16f
                        },
                        new
                        {
                            Id = 4,
                            Name = "Cards",
                            Width = 4.7f
                        },
                        new
                        {
                            Id = 5,
                            Name = "Mug",
                            Width = 94f
                        },
                        new
                        {
                            Id = 1,
                            Name = "PhotoBook",
                            Width = 19f
                        });
                });

            modelBuilder.Entity("TechnicalAssignment.Data.Entities.OrderProduct", b =>
                {
                    b.HasOne("TechnicalAssignment.Data.Entities.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechnicalAssignment.Data.Entities.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TechnicalAssignment.Data.Entities.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("TechnicalAssignment.Data.Entities.Product", b =>
                {
                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
