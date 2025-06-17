using AutoMapper;
using ChocoArtesanal.Application.Dtos;
using ChocoArtesanal.Domain.Entities;

namespace ChocoArtesanal.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
            .ForMember(dest => dest.ProducerName, opt => opt.MapFrom(src => src.Producer != null ? src.Producer.Name : string.Empty));
        CreateMap<Producer, ProducerDto>().ReverseMap();CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateOrderRequestDto, Order>();
        CreateMap<CreateOrderDetailDto, OrderDetail>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderDetail, OrderDetailDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty));
    }
}