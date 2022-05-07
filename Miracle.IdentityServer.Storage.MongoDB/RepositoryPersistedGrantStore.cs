using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;

namespace Miracle.IdentityServer.Storage.MongoDB;

public class RepositoryPersistedGrantStore : IPersistedGrantStore
{
    protected IRepository _repository;

    public RepositoryPersistedGrantStore(IRepository repository) => _repository = repository;

    public Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter) =>
        Task.FromResult(_repository.Where<PersistedGrant>(c => c.SubjectId == filter.SubjectId).AsEnumerable());

    public Task<PersistedGrant> GetAsync(string key) => Task.FromResult(_repository.Single<PersistedGrant>(i => i.Key == key));

    public Task RemoveAllAsync(PersistedGrantFilter filter)
    {
        _repository.Delete<PersistedGrantFilter>(i => i.SubjectId == filter.SubjectId && i.ClientId == filter.ClientId && i.Type == filter.Type);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key)
    {
        _repository.Delete<PersistedGrant>(i => i.Key == key);
        return Task.CompletedTask;
    }

    public Task StoreAsync(PersistedGrant grant)
    {
        _repository.Add(grant);
        return Task.CompletedTask;
    }
}
