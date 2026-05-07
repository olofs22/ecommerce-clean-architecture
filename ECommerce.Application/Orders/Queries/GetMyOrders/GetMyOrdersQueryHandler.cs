using AutoMapper;
using ECommerce.Application.Orders.DTOs;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Orders.Queries.GetMyOrders;

public class GetMyOrdersQueryHandler : IRequestHandler<GetMyOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetMyOrdersQueryHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetMyOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }
}