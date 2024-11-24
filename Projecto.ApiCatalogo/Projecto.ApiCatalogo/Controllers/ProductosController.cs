using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Projecto.ApiCatalogo.Context;
using Projecto.ApiCatalogo.DTOs;
using Projecto.ApiCatalogo.Filter;
using Projecto.ApiCatalogo.Models;
using Projecto.ApiCatalogo.Repositories._CategoriaRepository;
using Projecto.ApiCatalogo.Repositories._ProductoRepository;
using Projecto.ApiCatalogo.Repositories.GenericRepository;

namespace Projecto.ApiCatalogo.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductosController> _logger;

    public ProductosController(IUnitOfWork IUnitOfWork, ILogger<ProductosController> ILogger, IMapper mapper)
    {
        _unitOfWork = IUnitOfWork;
        _logger = ILogger;
        _mapper = mapper;
    }

    [HttpGet("/productosPorCategoria/{id}")]
    public ActionResult<IEnumerable<ProductoDTO>> GetProductosCategoria(int id)
    {
        var productos = _unitOfWork.ProductoRepository.GetProductosPorCategoria(id);
        if (productos is null)
        {
            _logger.LogInformation($"Productos ligados a categoria com o id = {id} não foram encontrados...");
            return NotFound($"Productos ligados a categoria com o id = {id} não foram encontrados...");
        }

        var produtosDtosDto = _mapper.Map<IEnumerable<ProductoDTO>>(productos);

        return Ok(produtosDtosDto);
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggerFilter))]
    public ActionResult<IEnumerable<ProductoDTO>> Get()
    {
        var productos = _unitOfWork.ProductoRepository.GetAll();

        if (productos is null)
        {
            _logger.LogWarning($"Productos não encontrados...");
            return NotFound($"Productos não encontrados...");
        }
        var produtosDto = _mapper.Map<IEnumerable<ProductoDTO>>(productos);

        return Ok(produtosDto);
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterProducto")]
    public ActionResult<ProductoDTO> Get(int id)
    {
        // throw new Exception("Exceção ao retornar a categoria pelo id");
        var producto = _unitOfWork.ProductoRepository.Get(p => p.ProductoId == id);
        if (producto is null)
        {
            _logger.LogInformation($"Producto com o id = {id} não encontrado...");
            return NotFound($"Producto com o id = {id} não encontrado...");
        }

        var produtoDto = _mapper.Map<ProductoDTO>(producto);
        return Ok(produtoDto);
    }

    [HttpPost]
    public ActionResult<ProductoDTO> Post(ProductoDTO productoDto)
    {
        if (productoDto == null)
        {
            _logger.LogWarning("Dados inválidos recebidos no POST.");
            return BadRequest("Dados inválidos.");
        }

        
        var produto = _mapper.Map<Producto>(productoDto);
        
        var novoProduto = _unitOfWork.ProductoRepository.Create(produto);
        _unitOfWork.Commit();
     
        var novoProdutoDto = _mapper.Map<ProductoDTO>(novoProduto);

        // Retornando o recurso recém-criado com o código de status 201 (Created)
        return CreatedAtRoute("ObterProducto", new { id = novoProdutoDto.ProductoId }, novoProdutoDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<ProductoDTO> Put(int id, ProductoDTO productoDto)
    {
        if (id != productoDto.ProductoId)
        {
            _logger.LogWarning($"O ID fornecido no corpo da requisição ({productoDto.ProductoId}) não corresponde ao ID da URL ({id}).");
            return BadRequest("Dados inválidos. O ID não corresponde.");
        }
      
        var produto = _mapper.Map<Producto>(productoDto);
       
        var produtoAtualizado = _unitOfWork.ProductoRepository.Update(produto);
        _unitOfWork.Commit();
      
        var produtoAtualizadoDto = _mapper.Map<ProductoDTO>(produtoAtualizado);
      
        _logger.LogInformation($"Produto com o ID = {id} foi atualizado.");

        // Retornando o produto atualizado com status 200 (OK)
        return Ok(produtoAtualizadoDto);
    }


    [HttpDelete("{id:int}")]
    public ActionResult<ProductoDTO> Delete(int id)
    {
        var producto = _unitOfWork.ProductoRepository.Get(p => p.ProductoId == id);
        if (producto is null)
        {
            _logger.LogInformation($"Falha ao excluir o producto com o id = {id}...");
            return StatusCode(500, $"Falha ao excluir o producto com o id = {id}...");
        }

        var productoRemovido = _unitOfWork.ProductoRepository.Delete(producto);
        _unitOfWork.Commit();

        var productoRemovidoDto = _mapper.Map<ProductoDTO>(productoRemovido);
        _logger.LogInformation($"O producto com o id = {id} foi excluido com sucesso...");
        
        return Ok(productoRemovidoDto);
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