using IdentityServer4.MongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IdentityServer4.MongoDB
{
    public interface IConfigurationMongoDbContext
    {
        IMongoCollection<ApiResource> ApiResource { get; }
        IMongoCollection<Client> Client { get; }
        IMongoCollection<IdentityResource> IdentityResource { get; }
    }

    public interface IOperationDbContext
    {
        IMongoCollection<PersistedGrant> PersistedGrant { get; }
    }

    public class ConfigurationMongoDbContext : IConfigurationMongoDbContext
    {
        public ConfigurationMongoDbContext(IOptions<ConfigurationDBOption> option)
        {
            var client = new MongoClient(option.Value.ConnectionString);
            var database = client.GetDatabase(option.Value.Database);

            Client = database.GetCollection<Client>(option.Value.Client.CollectionName);
            ApiResource = database.GetCollection<ApiResource>(option.Value.ApiResource.CollectionName);
            IdentityResource = database.GetCollection<IdentityResource>(option.Value.IdentityResource.CollectionName);
        }

        public IMongoCollection<ApiResource> ApiResource { get; private set; }
        public IMongoCollection<Client> Client { get; private set; }
        public IMongoCollection<IdentityResource> IdentityResource { get; private set; }
    }

    public class OperationDbContext : IOperationDbContext
    {
        public OperationDbContext(IOptions<OperationMongoDBOption> option)
        {
            var client = new MongoClient(option.Value.ConnectionString);
            var database = client.GetDatabase(option.Value.Database);

            PersistedGrant = database.GetCollection<PersistedGrant>(option.Value.PersistedGrant.CollectionName);
        }

        public IMongoCollection<PersistedGrant> PersistedGrant { get; private set; }
    }
}