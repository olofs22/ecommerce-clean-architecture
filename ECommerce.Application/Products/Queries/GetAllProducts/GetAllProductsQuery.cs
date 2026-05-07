using ECommerce.Application.Products.DTOs;
using MediatR;

namespace ECommerce.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;