using AutoMapper;
using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<OrderDto, Order>()
				.ForMember(d => d.OrderItems, opt => opt.Ignore())
				.ForMember(d => d.Payments, opt => opt.Ignore())
				.ForMember(d => d.Table, opt => opt.Ignore())
				.ForMember(d => d.Status, opt => opt.Ignore());
			CreateMap<Order, OrderDto>();
			CreateMap<OrderItem, OrderItemDto>();
			CreateMap<OrderItemDto, OrderItem>()
				.ForMember(d => d.Variant, opt => opt.Ignore());
			CreateMap<TableStatus, TableStatusDto>().ReverseMap();
			CreateMap<RestaurantTable, RestaurantTableDto>().ReverseMap();
			CreateMap<Payment, PaymentDto>().ReverseMap();
			CreateMap<ProductVariant, ProductVariantDto>();
			CreateMap<ProductVariantDto, ProductVariant>()
				.ForMember(d => d.Product, opt => opt.Ignore());
			CreateMap<Product, ProductDto>().ReverseMap();
			CreateMap<Category, CategoryDto>().ReverseMap();
			CreateMap<Brand, BrandDto>().ReverseMap();
			CreateMap<StockItem, StockItemDto>().ReverseMap();
			CreateMap<StockTransaction, StockTransactionDto>().ReverseMap();
			CreateMap<Ingredient, IngredientDto>().ReverseMap();
			CreateMap<ProductIngredient, ProductIngredientDto>().ReverseMap();

		}
	}

}
