using ProductCatalog.Domain.Entities;
using MediatR;

namespace ProductCatalog.Application.Products.Queries;
public class GetProductByIdQuery : IRequest<Product>
{
    public int? Id { get; set; }

    public GetProductByIdQuery(int? id)
    {
        if (id is not null)
        {
            Id = id;
        }
    }
}
