using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Products.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        var product = _mapper.Map<Product>(request);

        await _productRepository.AddAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);

        var saved = await _productRepository.GetByIdWithCategoryAsync(product.Id, cancellationToken);
        return _mapper.Map<ProductDto>(saved);
    }
}