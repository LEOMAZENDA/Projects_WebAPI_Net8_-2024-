using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projecto.ApiCatalogo.Context;
using Projecto.ApiCatalogo.Filter;
using Projecto.ApiCatalogo.Models;
using Projecto.ApiCatalogo.Repositories._CategoriaRepository;
using Projecto.ApiCatalogo.Repositories._ProductoRepository;

namespace Projecto.ApiCatalogo.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _repository;
    private readonly  ILogger<ProductosController> _logger;
    public ProductosController(IProductoRepository repository, ILogger<ProductosController> ILogger)
    {
        _repository = repository;
        _logger = ILogger;
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggerFilter ))]
    public  ActionResult<IEnumerable<Producto>> Get()
    {
        var productos = _repository.GetAll();
        
        if (productos is null) {
            _logger.LogWarning($"Productos não encontrados...");
            return NotFound($"Productos não encontrados...");
        }
        
        return Ok(productos);
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterProducto")]
    public ActionResult<Producto> Get(int id)
    {
        // throw new Exception("Exceção ao retornar a categoria pelo id");
        var producto = _repository.Get(p => p.ProductoId == id);
        if (producto is null) {
            _logger.LogInformation($"Producto com o id = {id} não encontrado...");
            return NotFound($"Producto com o id = {id} não encontrado...");
        }
        return Ok(producto);
    }

    [HttpGet("/productosPorCategoria/{id}")]
    public ActionResult<IEnumerable<Producto>> GetProductosCategoria(int id)
    {
       var productos = _repository.GetProductosPorCategoria(id);
       if (productos is null) {
           _logger.LogInformation($"Productos ligados a categoria com o id = {id} não foram encontrados...");
           return NotFound($"Productos ligados a categoria com o id = {id} não foram encontrados...");
       }
       return Ok(productos);
    }
    
    [HttpPost]
    public ActionResult<Producto> Post(Producto producto)
    {
        if (producto is null) {
            _logger.LogInformation($"Dados Inválidos...");
            return BadRequest($"Dados Inválidos...");
        }

        var novoProducto = _repository.Create(producto);

        return new CreatedAtRouteResult("ObterProducto",
            new { id = novoProducto.ProductoId }, novoProducto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Producto> Put(int id, Producto producto)
    {
        if (id != producto.ProductoId) {
            _logger.LogInformation($"Dados passados foram inválidos. Verifique por favor...");
            return BadRequest($"Dados inválidos. Verifique por favor...");//400
        }
        
        var prodActualizado = _repository.Update(producto);
        _logger.LogInformation($"Productos com o id = {id} foi actualizado...");
        
        return Ok(prodActualizado);
       
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Producto> Delete(int id)
    {
        var producto = _repository.Get(p => p.ProductoId == id);
        if (producto is null)
        {
            _logger.LogInformation($"Falha ao excluir o producto com o id = {id}...");
            return StatusCode(500, $"Falha ao excluir o producto com o id = {id}...");
        }

        var productoRemovido = _repository.Delete(producto);
        _logger.LogInformation($"O producto com o id = {id} foi excluido com sucesso...");
        return Ok(productoRemovido);
    }
    
    
    
    // // [HttpGet("primeiro")]
    // // [HttpGet("teste")]
    // // [HttpGet("/primeiro")]
    // [HttpGet("valor:alpha:length(5)")] // -- So recebe um tamanho maior que 5 caracteres
    // public async Task<ActionResult<Producto>> GetPrimeiro()
    // {
    //     try
    //     {
    //         var producto = await _context.Productos.FirstOrDefaultAsync();
    //         if (producto is null) return NotFound("Producto não encontrado");
    //
    //         return producto;
    //     }
    //     catch (Exception)
    //     {
    //         return StatusCode(StatusCodes.Status500InternalServerError,
    //             "Ocoorreu um problema ao tratar a sua solicitação. Tente de novo e se o problema persistir, consulte a equipa de suporte");
    //     }
    // }
}