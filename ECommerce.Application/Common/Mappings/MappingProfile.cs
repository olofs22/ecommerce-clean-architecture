using AutoMapper;
using ECommerce.Application.Products.DTOs;
using ECommerce.Application.Products.Commands.CreateProduct;
using ECommerce.Application.Products.Commands.UpdateProduct;
using ECommerce.Application.Categories.DTOs;
using ECommerce.Application.Categories.Commands.CreateCategory;
using ECommerce.Application.Orders.DTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();

        // Category
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryCommand, Category>();

        // Order
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
    }
}