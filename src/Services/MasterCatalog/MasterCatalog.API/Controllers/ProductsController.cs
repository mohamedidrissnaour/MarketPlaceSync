using MasterCatalog.Application.Commands.CreateProduct;
using MasterCatalog.Application.Commands.DeleteProduct;
using MasterCatalog.Application.Commands.UpdateProduct;
using MasterCatalog.Application.DTOs;
using MasterCatalog.Application.Queries.GetAllProducts;
using MasterCatalog.Application.Queries.GetProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MasterCatalog.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IMediator mediator , ILogger<ProductsController> logger) {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger  ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>) , StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
       
            _logger.LogInformation("GET /api/Products");
            var result = await _mediator.Send(new GetAllProductsQuery() , cancellationToken);
            return Ok(result);
       
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDto) , StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id , CancellationToken cancellationToken)
    {
        _logger.LogInformation("GET /api/Products/{Id}" , id);
        var result = await _mediator.Send(new GetProductByIdQuery(id) , cancellationToken);
        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(typeof(ProductDto) , StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command , CancellationToken cancellationToken)
    {
        _logger.LogInformation("POST /api/products SKU '{Sku}'" , command.Sku);
        var result = await _mediator.Send(command , cancellationToken);
        return CreatedAtAction(nameof(GetById) , new {id = result.Id} , result);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductDto) , StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id , [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PUT /api/products/{Id}" , id);
        var updatedCommand = command with {Id = id};
        var result = await _mediator.Send(updatedCommand , cancellationToken);
        return Ok(result);
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DELETE /api/products/{Id}", id);
        await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
        return NoContent();
    }



}