using Projecto.ApiCatalogo.Context;
using Projecto.ApiCatalogo.Models;
using Projecto.ApiCatalogo.Repositories._CategoriaRepository;
using Projecto.ApiCatalogo.Repositories.GenericRepository;

namespace Projecto.ApiCatalogo.Repositories._CategoriaRepository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
  public CategoriaRepository(AppDbContext context) : base(context) { }
}