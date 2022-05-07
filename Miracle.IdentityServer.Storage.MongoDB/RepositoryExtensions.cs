using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.Extensions.DependencyInjection;
using Miracle.MongoDB;

namespace Miracle.IdentityServer.Storage.MongoDB;

public static class RepositoryExtensions
{
    public static IIdentityServerBuilder AddMongoRepository(this IIdentityServerBuilder builder, BaseDbContext db)
    {
        _ = builder.Services.AddTransient<IRepository, MongoRepository>(s => new MongoRepository(db));
        return builder;
    }

    public static IIdentityServerBuilder AddIdentityClients(this IIdentityServerBuilder builder)
    {
        _ = builder.Services.AddTransient<IClientStore, RepositoryClientStore>();
        return builder;
    }

    public static IIdentityServerBuilder AddIdentityResources(this IIdentityServerBuilder builder)
    {
        _ = builder.Services.AddTransient<IResourceStore, RepositoryResourceStore>();
        return builder;
    }

    public static IIdentityServerBuilder AddIdentityPersistedGrants(this IIdentityServerBuilder builder)
    {
        _ = builder.Services.AddTransient<IPersistedGrantStore, RepositoryPersistedGrantStore>();
        return builder;
    }

    public static IIdentityServerBuilder AddPolicyService(this IIdentityServerBuilder builder)
    {
        _ = builder.Services.AddSingleton<ICorsPolicyService, RepositoryCorsPolicyService>();
        return builder;
    }
}
