using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;

namespace Duende.IdentityServer.Storage.MongoDB;

public class RepositoryResourceStore : IResourceStore
{
    protected IRepository _repository;

    public RepositoryResourceStore(IRepository repository) => _repository = repository;

    private IEnumerable<IdentityResource> GetAllIdentityResources() => _repository.All<IdentityResource>();
    private IEnumerable<ApiResource> GetAllApiResources() => _repository.All<ApiResource>();
    private IEnumerable<ApiScope> GetAllApiScopes() => _repository.All<ApiScope>();

    public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames) =>
        Task.FromResult(_repository.Where<ApiResource>(e => apiResourceNames.Contains(e.Name)).AsEnumerable());
    
    public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames) =>
        Task.FromResult(_repository.Where<ApiResource>(e => scopeNames.Contains(e.Name)).AsEnumerable());
    
    public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames) =>
        Task.FromResult(_repository.Where<ApiScope>(e => scopeNames.Contains(e.Name)).AsEnumerable());
    
    public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames) =>
        Task.FromResult(_repository.Where<IdentityResource>(e => scopeNames.Contains(e.Name)).AsEnumerable());
    
    public Task<Resources> GetAllResourcesAsync() => 
        Task.FromResult(new Resources(GetAllIdentityResources(), GetAllApiResources(), GetAllApiScopes()));
}