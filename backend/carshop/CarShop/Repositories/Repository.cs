using System.Linq.Expressions;
using CarShop.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace CarShop.Repositories;

public class Repository<T> : IRepository<T> where T : class {

    protected CarShopDataContext      _ctx;

    public Repository(CarShopDataContext context) {
        _ctx = context;
    }

    public IEnumerable<T> GetAll() {
        return _ctx.Set<T>().ToList();
    }

    public T? Get(Expression<Func<T, bool>> opt) {
        return _ctx.Set<T>().FirstOrDefault(opt);
    }

    public T Add(T entity) {
        _ctx.Set<T>().Add(entity);
        return entity;
    }

    public T Update(T entity) {
        _ctx.Entry(entity).State = EntityState.Modified;
        return entity;
    }

    public T Delete(T entity) {
        _ctx.Set<T>().Remove(entity);
        return entity;
    }
}