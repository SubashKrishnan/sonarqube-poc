using Microsoft.AspNetCore.Mvc;
using SonarDemo.Api.Models;
using SonarDemo.Api.Services;

namespace SonarDemo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<Product>> GetAll() => Ok(_service.GetAll());

    [HttpGet("{id:int}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _service.GetById(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create([FromBody] CreateProductRequest request)
    {
        var created = _service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
