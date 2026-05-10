using ECommerce.Application.Products.DTOs;
using MediatR;

namespace ECommerce.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    int CategoryId
) : IRequest<ProductDto>;