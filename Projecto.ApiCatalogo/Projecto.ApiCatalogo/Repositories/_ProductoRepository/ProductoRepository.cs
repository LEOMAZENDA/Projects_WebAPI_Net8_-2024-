using Microsoft.EntityFrameworkCore;
using Projecto.ApiCatalogo.Context;
using Projecto.ApiCatalogo.Models;
using Projecto.ApiCatalogo.Repositories.GenericRepository;

namespace Projecto.ApiCatalogo.Repositories._ProductoRepository;

public class ProductoRepository : Repository<Producto>, IProductoRepository
{
    public ProductoRepository(AppDbContext context = null) : base(context) { }


    public IEnumerable<Producto> GetProductosPorCategoria(int id)
    {
        return GetAll().Where(c => c.CategotiaId == id);
    }
}