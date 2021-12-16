using System.Linq;

using AutoMapper;

using TechnicalAssignment.Data.Entities;

namespace TechnicalAssignment.Data.Models.Mapping
{
    internal class MappingProfileDefault : Profile
    {
        public MappingProfileDefault()
        {
            CreateMap<Order, OrderRequestDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Order, OrderRequestWithProductsDto>()
                .AfterMap((src, dest, context) =>
                {
                    dest.Products = src.OrderProducts.Select(op => new OrderRequestProductDto { Id = op.ProductId, Quantity = op.Quantity });
                });

            CreateMap<OrderRequestWithProductsDto, Order>()
                .AfterMap((src, dest, context) =>
                {
                    dest.OrderProducts = src.Products.Select(p => new OrderProduct { OrderId = src.Id, ProductId = p.Id, Quantity = p.Quantity }).ToList();
                });

            CreateMap<OrderRequestProductDto, OrderResponseProductDto>();

            CreateMap<OrderRequestWithProductsDto, OrderResponseWithProductsDto>();

            CreateMap<OrderProduct, OrderProductDto>()
                .AfterMap((src, dest) =>
                {
                    dest.Id = src.Product.Id;
                    dest.StackSize = src.Product.StackSize;
                    dest.Width = src.Product.Width;
                });
            //CreateMap<OrderRequestWithProductsDto, OrderResponseWithProductsDto>()
            //    //.ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
            //    .ForMember(dest => dest.Products, opt => opt.Ignore())
            //    .AfterMap((src, dest, context) => context.Mapper.Map<>);
        }
    }
}
