using Projecto.ApiCatalogo.Models;
using Projecto.ApiCatalogo.Repositories.GenericRepository;

namespace Projecto.ApiCatalogo.Repositories._ProductoRepository;

public interface IProductoRepository : IRepository<Producto>
{
    IEnumerable<Producto> GetProductosPorCategoria(int id);
}