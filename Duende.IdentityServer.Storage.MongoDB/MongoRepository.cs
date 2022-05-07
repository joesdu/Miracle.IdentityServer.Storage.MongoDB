using Miracle.MongoDB;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Duende.IdentityServer.Storage.MongoDB;
public class MongoRepository : IRepository
{
    private readonly IMongoDatabase _database;

    public MongoRepository(BaseDbContext db) => _database = db._database!;

    public IQueryable<T> All<T>() where T : class, new() => _database.GetCollection<T>(typeof(T).Name).AsQueryable();

    public IQueryable<T> Where<T>(Expression<Func<T, bool>> expression) where T : class, new() => All<T>().Where(expression);
    
    public void Delete<T>(Expression<Func<T, bool>> expression) where T : class, new() => _database.GetCollection<T>(typeof(T).Name).DeleteMany(expression);

    public T Single<T>(Expression<Func<T, bool>> expression) where T : class, new() => All<T>().Where(expression).SingleOrDefault() ?? throw new();

    public void Add<T>(T item) where T : class, new() => _database.GetCollection<T>(typeof(T).Name).InsertOne(item);

    public void Add<T>(IEnumerable<T> items) where T : class, new() => _database.GetCollection<T>(typeof(T).Name).InsertMany(items);
}
