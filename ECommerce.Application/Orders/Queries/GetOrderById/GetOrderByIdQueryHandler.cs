using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Orders.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdWithItemsAsync(request.Id, cancellationToken);
        if (order is null)
        {
            throw new NotFoundException(nameof(Order), request.Id);
        }

        return _mapper.Map<OrderDto>(order);
    }
}