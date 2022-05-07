using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;

namespace Miracle.IdentityServer.Storage.MongoDB;
internal class RepositoryClientStore : IClientStore
{
    protected IRepository _repository;

    public RepositoryClientStore(IRepository repository) => _repository = repository;
    public Task<Client> FindClientByIdAsync(string clientId) =>
        Task.FromResult(_repository.Single<Client>(c => c.ClientId == clientId));
}
