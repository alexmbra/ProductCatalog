using ProductCatalog.Application.Products.Commands;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using MediatR;

namespace ProductCatalog.Application.Products.Handlers;
public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
{
    private readonly IRepository<Product> _productRepository;

    public ProductCreateCommandHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name ?? string.Empty, request.Description ?? string.Empty, request.Price, request.Stock, request.Image ?? string.Empty);
        if (product is null)
        {
            throw new ApplicationException("Error creating entity");
        }
        else
        {
            product.CategoryId = request.CategoryId;
            return await _productRepository.CreateAsync(product);
        }
    }
}
