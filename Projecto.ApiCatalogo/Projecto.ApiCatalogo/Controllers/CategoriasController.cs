using Microsoft.AspNetCore.Mvc;
using Projecto.ApiCatalogo.Models;
using Projecto.ApiCatalogo.Repositories._CategoriaRepository;
using Projecto.ApiCatalogo.Repositories.GenericRepository;


namespace Projecto.ApiCatalogo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase 
{
    private readonly IRepository<Categoria> _repository;
    private readonly  ILogger<CategoriasController> _logger;
    public CategoriasController(ICategoriaRepository repository, ILogger<CategoriasController> ILogger)
    {
        _repository = repository;
        _logger = ILogger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        //throw new Exception("Exceção ao retornar a categoria pelo id");
        var categorias = _repository.GetAll();
        
        if (categorias is null) {
            _logger.LogWarning($"Categorias não encontradas...");
            return NotFound($"Categorias não encontradas...");
        }
        
        return Ok(categorias);
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        // throw new Exception("Exceção ao retornar a categoria pelo id");
        var categorias = _repository.Get(c => c.CategoriaId == id);
        if (categorias is null) {
            _logger.LogInformation($"Categoria com o id = {id} não encontrada...");
            return NotFound($"Categoria com o id = {id} não encontrada...");
        }
        return Ok(categorias);
    }
 
    [HttpPost]
    public ActionResult<Categoria> Post(Categoria categoria)
    {
        if (categoria is null) {
            _logger.LogInformation($"Dados Inválidos...");
            return BadRequest($"Dados Inválidos...");
        }

        var categoriaCriada = _repository.Create(categoria);

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoriaCriada.CategoriaId }, categoriaCriada);
    }


    [HttpPut("{id:int}")]
    public ActionResult<Categoria> Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId) {
            _logger.LogInformation($"Dados inválidos. Verifique por favor...");
            return BadRequest($"Dados inválidos. Verifique por favor...");
        }
        
        _repository.Update(categoria);
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _repository.Get(c => c.CategoriaId == id);

        if (categoria is null) {
            _logger.LogInformation($"Categoria com o id = {id} não encontrada...");
            return NotFound($"Categoria com o id = {id} não encontrada...");
        }

        var categoriaRemovida = _repository.Delete(categoria);
        return Ok(categoriaRemovida);
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