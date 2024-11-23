using Projecto.ApiCatalogo.Repositories._CategoriaRepository;
using Projecto.ApiCatalogo.Repositories._ProductoRepository;

namespace Projecto.ApiCatalogo.Repositories.GenericRepository
{
    public interface IUnitOfWork
    {
        ICategoriaRepository CategoriaRepository { get; }
        IProductoRepository ProductoRepository { get; }

        void Commit();
    }
}