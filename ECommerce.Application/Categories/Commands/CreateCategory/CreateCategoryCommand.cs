using ECommerce.Application.Categories.DTOs;
using MediatR;

namespace ECommerce.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, string? Description) : IRequest<CategoryDto>;