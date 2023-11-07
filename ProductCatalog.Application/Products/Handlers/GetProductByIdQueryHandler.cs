using ProductCatalog.Application.Products.Queries;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using MediatR;


namespace ProductCatalog.Application.Products.Handlers;
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IRepository<Product> _productRepository;

    public GetProductByIdQueryHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        return product ?? throw new("Product not found.");
    }
}
