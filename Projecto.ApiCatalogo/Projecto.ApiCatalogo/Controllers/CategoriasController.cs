using Microsoft.AspNetCore.Mvc;
using Projecto.ApiCatalogo.DTOs;
using Projecto.ApiCatalogo.Extensions;
using Projecto.ApiCatalogo.Models;
using Projecto.ApiCatalogo.Repositories.GenericRepository;


namespace Projecto.ApiCatalogo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uniyOfWork;
    private readonly ILogger<CategoriasController> _logger;
    public CategoriasController(IUnitOfWork IUnitOfWork, ILogger<CategoriasController> ILogger)
    {
        _uniyOfWork = IUnitOfWork;
        _logger = ILogger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CategoriaDTO>> Get()
    {
        //throw new Exception("Exceção ao retornar a categoria pelo id");
        var categorias = _uniyOfWork.CategoriaRepository.GetAll();

        if (categorias is null)
        {
            _logger.LogWarning($"Não existem categorias...");
            return NotFound($"Não existem categorias...");
        }

        var categoriasDTto = categorias.ToCategoriaDTOList();

        return Ok(categoriasDTto);
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
        // throw new Exception("Exceção ao retornar a categoria pelo id");
        var categoria = _uniyOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);
        if (categoria is null)
        {
            _logger.LogInformation($"Categoria com o id = {id} não encontrada...");
            return NotFound($"Categoria com o id = {id} não encontrada...");
        }

        var categoriaDto = categoria.ToCategoriaDTO();
        return Ok(categoriaDto);
    }

    [HttpPost]
    public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
    {
        if (categoriaDto is null)
        {
            _logger.LogInformation($"Dados Inválidos...");
            return BadRequest($"Dados Inválidos...");
        }

        var categoria = categoriaDto.ToCategoria();

        var categoriaCriada = _uniyOfWork.CategoriaRepository.Create(categoria);
        _uniyOfWork.Commit();

        var novaCategoriaDto = categoriaCriada.ToCategoriaDTO();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);
    }


    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.CategoriaId)
        {
            _logger.LogInformation($"Dados inválidos. Verifique por favor...");
            return BadRequest($"Dados inválidos. Verifique por favor...");
        }
        
        var categoria = categoriaDto.ToCategoria();

       var categoriaActualizada = _uniyOfWork.CategoriaRepository.Update(categoria);
        _uniyOfWork.Commit();

        var categoriaActualizadaDto = categoriaActualizada.ToCategoriaDTO();

        return Ok(categoriaActualizadaDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = _uniyOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            _logger.LogInformation($"Categoria com o id = {id} não encontrada...");
            return NotFound($"Categoria com o id = {id} não encontrada...");
        }

        var categoriaRemovida = _uniyOfWork.CategoriaRepository.Delete(categoria);
        _uniyOfWork.Commit();

        var categoriaRemovidaDto = categoriaRemovida.ToCategoriaDTO();

        return Ok(categoriaRemovidaDto);
    }


    // [HttpGet("LerArquivoConfiguracao")]
    // public string GetValores()
    // {
    //     var valor1 = _configuration["chave1"];
    //     var valor2 = _configuration["chave2"];
    //     var secao1 = _configuration["secao1:chave2"];
    //     return $"Chave1 = {valor1}  \nChave2 = {valor2} \nSeção1 => Chave2 = {secao1}";
    // }

    // [HttpGet("UsandoFromServices/{nome}")]
    // public ActionResult<string> GetUsaudacaoFromServices([FromServices] IMeuServico meuServico, string nome)
    // {
    //     return meuServico.Saudacao(nome);
    // }
    //
    // [HttpGet("SemUsarFromServices/{nome}")]
    // public ActionResult<string> GetSemUsarFromServices(IMeuServico meuServico, string nome)
    // {
    //     return meuServico.Saudacao(nome);
    // }
    //

}