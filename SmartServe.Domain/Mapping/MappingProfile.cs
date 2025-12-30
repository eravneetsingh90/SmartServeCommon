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
			.ForMember(d => d.Status, opt => opt.Ignore());
			CreateMap<Order, OrderDto>();

			CreateMap<OrderItem, OrderItemDto>();
			CreateMap<OrderItemDto, OrderItem>();

			CreateMap<TableStatus, TableStatusDto>();
			CreateMap<TableStatusDto, TableStatus>();

			CreateMap<RestaurantTable, RestaurantTableDto>();
			CreateMap<RestaurantTableDto, RestaurantTable>();

			CreateMap<Payment, PaymentDto>();
			CreateMap<PaymentDto, Payment>();

			CreateMap<ProductVariant, ProductVariantDto>();
			CreateMap<ProductVariantDto, ProductVariant>();
		}
	}

}
