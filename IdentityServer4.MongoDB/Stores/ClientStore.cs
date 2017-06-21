using IdentityServer4.Models;
using IdentityServer4.Stores;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB.Stores
{
    public class ClientStore : IClientStore
    {
        private readonly IConfigurationMongoDbContext _dbContext;

        public ClientStore(IConfigurationMongoDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var filter = Builders<Models.Client>.Filter.Eq(u => u.ClientId, clientId);

            var client = await _dbContext.Client.Find(filter).SingleOrDefaultAsync();

            return client.ToModel();
        }
    }
}