using ManagementInvoices.Application.Features.Products.Commands.CreateProduct;
using ManagementInvoices.Application.Features.Products.Commands.DeleteProduct;
using ManagementInvoices.Application.Features.Products.Commands.UpdateProduct;
using ManagementInvoices.Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementInvoices.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator) { _mediator = mediator; }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = productId }, null);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null) return NotFound();
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL and in body must match");

            await _mediator.Send(command);
            return NoContent();
        }

    }

}
