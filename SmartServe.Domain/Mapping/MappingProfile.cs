using AutoMapper;
using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Order, OrderEntity>()
				.ForMember(d => d.OrderItems, opt => opt.Ignore())
				.ForMember(d => d.Payments, opt => opt.Ignore())
				.ForMember(d => d.Table, opt => opt.Ignore())
				.ForMember(d => d.Status, opt => opt.Ignore());
			CreateMap<OrderEntity, Order>();
			CreateMap<OrderItemEntity, OrderItem>();
			CreateMap<OrderItem, OrderItemEntity>()
				.ForMember(d => d.Variant, opt => opt.Ignore());
			CreateMap<TableStatusEntity, TableStatus>().ReverseMap();
			CreateMap<RestaurantTableEntity, RestaurantTable>().ReverseMap();
			CreateMap<PaymentEntity, Payment>().ReverseMap();
			CreateMap<ProductVariantEntity, ProductVariant>();
			CreateMap<ProductVariant, ProductVariantEntity>()
				.ForMember(d => d.Product, opt => opt.Ignore());
			CreateMap<ProductEntity, Product>().ReverseMap();
			CreateMap<CategoryEntity, Category>().ReverseMap();
			CreateMap<BrandEntity, BrandDto>().ReverseMap();
			CreateMap<StockEntity, Stock>().ReverseMap();
			CreateMap<StockTransactionEntity, StockTransaction>().ReverseMap();
			CreateMap<IngredientEntity, Ingredient>().ReverseMap();
			CreateMap<ProductIngredientEntity, ProductIngredient>().ReverseMap();

		}
	}

}
