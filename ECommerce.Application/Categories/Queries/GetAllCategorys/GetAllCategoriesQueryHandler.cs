using ECommerce.Application.Categories.DTOs;
using MediatR;

namespace ECommerce.Application.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery() : IRequest<IEnumerable<CategoryDto>>;