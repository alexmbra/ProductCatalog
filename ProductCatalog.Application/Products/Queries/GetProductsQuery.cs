﻿using ProductCatalog.Domain.Entities;
using MediatR;

namespace ProductCatalog.Application.Products.Queries;
public class GetProductsQuery : IRequest<IEnumerable<Product>>
{
}
