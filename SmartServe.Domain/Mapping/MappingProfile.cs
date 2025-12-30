using AutoMapper;
using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Mapping
{
public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			//CreateMap<OrderDto, Order>()
			//	.ForMember(dest => dest.Status, opt => opt.Ignore());

			//CreateMap<Order, OrderDto>();
			CreateMap<OrderDto, Order>()
				.ForMember(d => d.OrderItems, opt => opt.Ignore())
				.ForMember(d => d.Payments, opt => opt.Ignore())
				.ForMember(d => d.Table, opt => opt.Ignore())
				.ForMember(d => d.Status, opt => opt.Ignore());
			CreateMap<Order, OrderDto>();

			CreateMap<OrderItem, OrderItemDto>();
			CreateMap<OrderItemDto, OrderItem>()
				.ForMember(d => d.Variant, opt => opt.Ignore());

			//CreateMap<List<OrderItem>, List<OrderItemDto>>();
			//CreateMap< List<OrderItemDto>, List<OrderItem>>();

			CreateMap<TableStatus, TableStatusDto>();
			CreateMap<TableStatusDto, TableStatus>();

			CreateMap<RestaurantTable, RestaurantTableDto>();
			CreateMap<RestaurantTableDto, RestaurantTable>();

			CreateMap<Payment, PaymentDto>();
			CreateMap<PaymentDto, Payment>();

			CreateMap<ProductVariant, ProductVariantDto>();
			CreateMap<ProductVariantDto, ProductVariant>()
				.ForMember(d => d.Product, opt => opt.Ignore());

			CreateMap<Product, ProductDto>();
			CreateMap<ProductDto, Product>();
			
		}
	}

}
