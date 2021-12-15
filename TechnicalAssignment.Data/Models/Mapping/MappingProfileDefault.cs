using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using TechnicalAssignment.Data.Entities;

namespace TechnicalAssignment.Data.Models.Mapping
{
    internal class MappingProfileDefault : Profile
    {
        public MappingProfileDefault()
        {
            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Order, OrderWithProductsDto>()
                .AfterMap((src, dest, context) =>
                {
                    dest.Products = src.OrderProducts.Select(op => new OrderProductDto { Id = op.ProductId, Quantity = op.Quantity });
                });

            CreateMap<OrderWithProductsDto, Order>()
                .AfterMap((src, dest, context) =>
                {
                    dest.OrderProducts = src.Products.Select(p => new OrderProduct { OrderId = src.Id, ProductId = p.Id, Quantity = p.Quantity }).ToList();
                });
        }
    }
}
