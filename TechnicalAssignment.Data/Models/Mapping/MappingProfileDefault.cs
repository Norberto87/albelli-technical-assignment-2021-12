using System.Linq;

using AutoMapper;

using TechnicalAssignment.Data.Entities;

namespace TechnicalAssignment.Data.Models.Mapping
{
    internal class MappingProfileDefault : Profile
    {
        public MappingProfileDefault()
        {
            CreateMap<Order, OrderRequestDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Order, OrderStatusDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Order, OrderRequestWithProductsDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .AfterMap((src, dest, context) =>
                {
                    dest.Products = src.OrderProducts.Select(op => new OrderRequestProductDto { ProductType = op.ProductId, Quantity = op.Quantity });
                });

            CreateMap<OrderRequestWithProductsDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId))
                .AfterMap((src, dest, context) =>
                {
                    dest.OrderProducts = src.Products.Select(p => new OrderProduct { OrderId = src.OrderId, ProductId = p.ProductType, Quantity = p.Quantity }).ToList();
                });

            CreateMap<Order, OrderResponseWithProductsDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status))
                .AfterMap((src, dest, context) =>
                {
                    dest.Products = src.OrderProducts.Select(op => new OrderResponseProductDto { ProductType = op.ProductId, Quantity = op.Quantity, BinWidth = op.Product.Width });
                });

            CreateMap<OrderRequestProductDto, OrderResponseProductDto>();

            CreateMap<OrderRequestWithProductsDto, OrderResponseWithProductsDto>();

            CreateMap<OrderProduct, OrderProductDto>()
                .AfterMap((src, dest) =>
                {
                    dest.ProductType = src.Product.Id;
                    dest.StackSize = src.Product.StackSize;
                    dest.Width = src.Product.Width;
                });
        }
    }
}
