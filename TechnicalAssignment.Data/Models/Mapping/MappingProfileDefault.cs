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
        }
    }
}
