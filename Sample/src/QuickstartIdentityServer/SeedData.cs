using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using IdentityServer4;
using IdentityServer4.MongoDB;

namespace QuickstartIdentityServer
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Seeding database...");

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IConfigurationMongoDbContext>();
                var count = context.Client.Count(Builders<IdentityServer4.MongoDB.Models.Client>.Filter.Empty);

                if (count == 0)
                {
                    foreach (var client in Config.GetClients().ToList())
                    {
                        context.Client.InsertOne(client.ToEntity());
                    }
                }

                count = context.IdentityResource.Count(Builders<IdentityServer4.MongoDB.Models.IdentityResource>.Filter.Empty);
                if (count == 0)
                {
                    foreach (var resource in Config.GetIdentityResources().ToList())
                    {
                        context.IdentityResource.InsertOne(resource.ToEntity());
                    }
                }

                count = context.ApiResource.Count(Builders<IdentityServer4.MongoDB.Models.ApiResource>.Filter.Empty);
                if (count == 0)
                {
                    foreach (var resource in Config.GetApiResources().ToList())
                    {
                        context.ApiResource.InsertOne(resource.ToEntity());
                    }
                }
            }

            Console.WriteLine("Done seeding database.");
            Console.WriteLine();
        }
    }
}
