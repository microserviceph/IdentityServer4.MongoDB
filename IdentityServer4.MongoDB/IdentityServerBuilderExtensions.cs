using IdentityServer4.MongoDB;
using IdentityServer4.MongoDB.Stores;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServerBuilderExtensionsMongoDB
    {
        public static IIdentityServerBuilder AddConfigurationStore(this IIdentityServerBuilder builder,
            Action<ConfigurationDBOption> storeOptionsAction = null)
        {
            // var options = new ConfigurationDBOption();
            // storeOptionsAction?.Invoke(options);
            // builder.Services.AddSingleton(options);
            builder.Services.Configure<ConfigurationDBOption>(storeOptionsAction);

            builder.Services.AddTransient<IConfigurationMongoDbContext, ConfigurationMongoDbContext>();

            builder.Services.AddTransient<IClientStore, ClientStore>();
            builder.Services.AddTransient<IResourceStore, ResourceStore>();
            builder.Services.AddTransient<ICorsPolicyService, CorsPolicyService>();

            return builder;
        }

        public static IIdentityServerBuilder AddOperationalStore(
            this IIdentityServerBuilder builder,
            Action<OperationMongoDBOption> storeOptionsAction = null)
        {
            // var options = new OperationMongoDBOption();
            // storeOptionsAction?.Invoke(options);
            builder.Services.Configure<OperationMongoDBOption>(storeOptionsAction);
            // builder.Services.AddSingleton(options);

            builder.Services.AddTransient<IOperationDbContext, OperationDbContext>();

            builder.Services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();

            builder.Services.AddSingleton<TokenCleanup>();
            builder.Services.AddSingleton<IHostedService, TokenCleanupHost>();

            return builder;
        }
    }
}