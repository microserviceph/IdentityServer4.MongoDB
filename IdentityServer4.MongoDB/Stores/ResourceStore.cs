using IdentityServer4.Models;
using IdentityServer4.Stores;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB.Stores
{
    public class ResourceStore : IResourceStore
    {
        private readonly IConfigurationMongoDbContext _dbContext;

        public ResourceStore(IConfigurationMongoDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var filter = Builders<Models.ApiResource>.Filter.Eq(u => u.Name, name);

            var found = await _dbContext.ApiResource.Find(filter).SingleOrDefaultAsync();

            return found.ToModel();
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var names = scopeNames.ToArray();
            /*
            var apis =
                from api in _dbContext.ApiResource.AsQueryable()
                where api.Scopes.Where(x => names.Contains(x.Name)).Any()
                select api;
             or this?   

            var filter = Builders<ApiResource>.Filter.Where(p => p.Scopes.Any(b => scopeNames.Contains(b.Name)));
             */

            var records = await _dbContext.ApiResource
                .Find( u=> u.Scopes.Any( s => names.Contains(s.Name)))
                .ToListAsync();

            return records.Select(Mapper.ToModel)
                .ToList();
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var filter = Builders<Models.IdentityResource>.Filter.In(p => p.Name, scopeNames);

            var records = await _dbContext.IdentityResource.Find(filter).ToListAsync();

            return records.Select(Mapper.ToModel)
                .ToList();
        }

        public async Task<Resources> GetAllResources()
        {
            var allApiFilter = Builders<Models.ApiResource>.Filter.Empty;
            var allIndenityFilter = Builders<Models.IdentityResource>.Filter.Empty;

            var apiResource = await _dbContext.ApiResource.Find(allApiFilter).ToListAsync();
            var identityResource = await _dbContext.IdentityResource.Find(allIndenityFilter).ToListAsync();

            return new Resources(identityResource.Select(Mapper.ToModel), apiResource.Select(Mapper.ToModel));
        }
    }
}