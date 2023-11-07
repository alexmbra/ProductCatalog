using ProductCatalog.Application.Products.Commands;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using MediatR;

namespace ProductCatalog.Application.Products.Handlers;
public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
{
    private readonly IRepository<Product> _productRepository;

    public ProductRemoveCommandHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null)
        {
            throw new ArgumentException("Error could not be found.");
        }
        else
        {
            var result = await _productRepository.RemoveAsync(product);

            return result;
        }
    }
}
