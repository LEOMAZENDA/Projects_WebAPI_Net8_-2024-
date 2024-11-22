using System.Linq.Expressions;

namespace Projecto.ApiCatalogo.Repositories.GenericRepository;

public interface IRepository<T>
{
    //Não violar o princípio ISP
    // IQueryable<T> GetAll();
    // T GetById(int id);
    IEnumerable<T> GetAll();
    T? Get(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}