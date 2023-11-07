using ProductCatalog.Application.Products.Queries;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using MediatR;

namespace ProductCatalog.Application.Products.Handlers;
public class GetProductByIdQueryDapperHandler : IRequestHandler<GetProductByIdQueryDapper, Product>
{
    private readonly IDapperRepository<Product> _dapperRepository;

    public GetProductByIdQueryDapperHandler(IDapperRepository<Product> dapperRepository)
    {
        _dapperRepository = dapperRepository ?? throw new ArgumentNullException(nameof(dapperRepository));
    }

    public async Task<Product> Handle(GetProductByIdQueryDapper request, CancellationToken cancellationToken)
    {
        var product = await _dapperRepository.GetByIdAsync(request.Id);

        if (product == null)
        {
            throw new Exception("Product not found.");
        }

        return product;
    }
}
