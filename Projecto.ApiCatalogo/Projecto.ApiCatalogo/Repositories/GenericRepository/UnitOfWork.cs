using Projecto.ApiCatalogo.Context;
using Projecto.ApiCatalogo.Repositories._CategoriaRepository;
using Projecto.ApiCatalogo.Repositories._ProductoRepository;

namespace Projecto.ApiCatalogo.Repositories.GenericRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICategoriaRepository? _categoriaRepo;
        private IProductoRepository? _productoRepo;

        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
                
                // if (_categoriaRepo is null)
                //     _categoriaRepo = new CategoriaRepository(_context);
                // return _categoriaRepo;
            }          
        }


        public IProductoRepository ProductoRepository
        {
            get { 
                    return _productoRepo = _productoRepo ?? new ProductoRepository(_context);
                    
                    // if (_productoRepo is null)
                    //     _productoRepo = new ProductoRepository(_context);
                    // return _productoRepo;

            }          
        }
        

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}