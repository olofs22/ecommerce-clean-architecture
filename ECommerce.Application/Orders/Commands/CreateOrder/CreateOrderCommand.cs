using ECommerce.Application.Orders.DTOs;
using MediatR;

namespace ECommerce.Application.Orders.Commands.CreateOrder;

public record OrderItemRequest(int ProductId, int Quantity);

public record CreateOrderCommand(string UserId, List<OrderItemRequest> Items) : IRequest<OrderDto>;