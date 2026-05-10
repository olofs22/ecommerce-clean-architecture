using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Orders.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            UserId = request.UserId,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            Items = new List<OrderItem>()
        };

        decimal total = 0m;

        foreach (var requested in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(requested.ProductId, cancellationToken);
            if (product is null)
            {
                throw new NotFoundException(nameof(Product), requested.ProductId);
            }

            if (product.Stock < requested.Quantity)
            {
                throw new InvalidOperationException(
                    $"Not enough stock for product '{product.Name}'. Available: {product.Stock}, requested: {requested.Quantity}.");
            }

            var item = new OrderItem
            {
                ProductId = product.Id,
                Quantity = requested.Quantity,
                UnitPrice = product.Price
            };

            order.Items.Add(item);
            total += item.UnitPrice * item.Quantity;

            product.Stock -= requested.Quantity;
            _productRepository.Update(product);
        }

        order.TotalAmount = total;

        await _orderRepository.AddAsync(order, cancellationToken);
        await _orderRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderDto>(order);
    }
}