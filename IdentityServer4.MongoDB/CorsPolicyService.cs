using IdentityServer4.Services;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB
{
    public class CorsPolicyService : ICorsPolicyService
    {
        private readonly IConfigurationMongoDbContext _dbContext;

        public CorsPolicyService(IConfigurationMongoDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            var records = await _dbContext.Client.FindAsync(u => u.AllowedCorsOrigins.Contains(origin));
            return records.FirstOrDefault() != null;
        }
    }
}