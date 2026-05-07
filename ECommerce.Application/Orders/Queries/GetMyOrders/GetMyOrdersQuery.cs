using ECommerce.Application.Orders.DTOs;
using MediatR;

namespace ECommerce.Application.Orders.Queries.GetMyOrders;

public record GetMyOrdersQuery(string UserId) : IRequest<IEnumerable<OrderDto>>;