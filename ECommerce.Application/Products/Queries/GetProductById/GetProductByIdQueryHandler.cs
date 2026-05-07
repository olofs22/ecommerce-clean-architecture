using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Products.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdWithCategoryAsync(request.Id, cancellationToken);
        if (product is null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        return _mapper.Map<ProductDto>(product);
    }
}