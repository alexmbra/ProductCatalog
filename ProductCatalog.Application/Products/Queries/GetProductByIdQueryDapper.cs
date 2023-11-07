using ProductCatalog.Domain.Entities;
using MediatR;

namespace ProductCatalog.Application.Products.Queries;
public class GetProductByIdQueryDapper : IRequest<Product>
{
    public int? Id { get; set; }

    public GetProductByIdQueryDapper(int? id)
    {
        if (id is not null)
        {
            Id = id;
        }
    }
}
