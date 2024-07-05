using System.Linq.Expressions;

namespace CarShop.Repositories;

public interface IRepository<T> {
    IEnumerable<T> GetAll();
    T? Get(Expression<Func<T, bool>> opt);
    T? Add(T entity);
    T? Update(T entity);
    T? Delete(T entity);
}