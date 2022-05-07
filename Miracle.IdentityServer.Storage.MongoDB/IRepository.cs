using System.Linq.Expressions;

namespace Miracle.IdentityServer.Storage.MongoDB;
public interface IRepository
{
    IQueryable<T> All<T>() where T : class, new();
    IQueryable<T> Where<T>(Expression<Func<T, bool>> expression) where T : class, new();
    T Single<T>(Expression<Func<T, bool>> expression) where T : class, new();
    void Delete<T>(Expression<Func<T, bool>> expression) where T : class, new();
    void Add<T>(T item) where T : class, new();
    void Add<T>(IEnumerable<T> items) where T : class, new();
}
