using AutoMapper;
using ChocoArtesanal.Application.Dtos;
using ChocoArtesanal.Domain.Entities;

namespace ChocoArtesanal.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product Mappings
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.ProducerName, opt => opt.MapFrom(src => src.Producer.Name));
        CreateMap<ProductDto, Product>();

        // Category Mappings
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();

        // Order Mappings
        CreateMap<OrderDetail, OrderDetailDto>();
        CreateMap<CreateOrderDetailDto, OrderDetail>();

        CreateMap<Order, OrderDto>();
        CreateMap<CreateOrderRequestDto, Order>();

        // User/Auth Mappings
        CreateMap<User, UserDto>();
        CreateMap<RegisterRequestDto, User>();
    }
}