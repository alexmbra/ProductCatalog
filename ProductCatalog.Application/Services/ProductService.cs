using AutoMapper;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Application.Products.Commands;
using ProductCatalog.Application.Products.Queries;
using MediatR;

namespace ProductCatalog.Application.Services;
public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProductService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
        var productsQuery = new GetProductsQueryDapper() ?? throw new Exception("Entity could not be loaded");
        var result = await _mediator.Send(productsQuery);
        return _mapper.Map<IEnumerable<ProductDTO>>(result);
    }

    public async Task<ProductDTO> GetProductByIdAsync(int? id)
    {
        if (id is null)
        {
            throw new Exception("Entity could not be loaded");
        }

        var productByIdQuery = new GetProductByIdQueryDapper(id.Value) ?? throw new Exception("Entity could not be loaded");
        var result = await _mediator.Send(productByIdQuery);
        return _mapper.Map<ProductDTO>(result);
    }

    public async Task Add(ProductDTO productDTO)
    {
        var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
        await _mediator.Send(productCreateCommand);
    }

    public async Task Update(ProductDTO productDTO)
    {
        var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
        await _mediator.Send(productUpdateCommand);
    }

    public async Task Remove(int? id)
    {
        if (id is null)
        {
            throw new Exception("Entity could not be loaded");
        }

        var productRemoveCommand = new ProductRemoveCommand(id.Value) ?? throw new Exception("Entity could not be loaded");
        await _mediator.Send(productRemoveCommand);
    }
}
