using ProductCatalog.Application.Products.Commands;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using MediatR;

namespace ProductCatalog.Application.Products.Handlers;
public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
{
    private readonly IRepository<Product> _productRepository;

    public ProductUpdateCommandHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null)
        {
            throw new ArgumentException("Error could not be found.");
        }
        else
        {
            product.Update(request.Name ?? string.Empty, request.Description ?? string.Empty, request.Price, request.Stock, request.Image ?? string.Empty, request.CategoryId);

            return await _productRepository.UpdateAsync(product);
        }

    }
}
