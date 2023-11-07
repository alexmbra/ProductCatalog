using ProductCatalog.Application.Products.Queries;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using MediatR;

namespace ProductCatalog.Application.Products.Handlers;
public class GetProductsQueryDapperHandler : IRequestHandler<GetProductsQueryDapper, IEnumerable<Product>>
{
    private readonly IRepository<Product> _productRepository;

    public GetProductsQueryDapperHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<IEnumerable<Product>> Handle(GetProductsQueryDapper request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetAllAsync();
    }
}
