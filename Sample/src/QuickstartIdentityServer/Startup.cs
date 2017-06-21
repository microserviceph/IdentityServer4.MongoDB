// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Linq;
using System.Reflection;

namespace QuickstartIdentityServer
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            /*var connectionString = @"server=(localdb)\mssqllocaldb;database=IdentityServer4.QuickStart.EntityFramework;trusted_connection=yes";
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;*/

            // configure identity server with in-memory users, but EF stores for clients and scopes
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddTestUsers(Config.GetUsers())
                .ConfigureConfigurationDBOption(option =>
                {
                    option.ConnectionString = "mongodb://192.168.103.115:27017";
                    option.Database = "IdentityServer";
                })
                .ConfigureOperationMongoDBOption(option =>
                {
                    option.ConnectionString = "mongodb://192.168.103.115:27017";
                    option.Database = "IdentityServer";
                })
                .AddConfigurationStore()
                .AddOperationalStore();

                /*.AddConfigurationStore(builder =>
                    builder.UseSqlServer(connectionString, options =>
                        options.MigrationsAssembly(migrationsAssembly)))
                .AddOperationalStore(builder =>
                    builder.UseSqlServer(connectionString, options =>
                        options.MigrationsAssembly(migrationsAssembly)));*/
        }

        public void Configure(IApplicationBuilder app, IApplicationLifetime applicationLifetime, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // this will do the initial DB population
            InitializeDatabase(app);

            loggerFactory.AddConsole(LogLevel.Debug);
            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();
            app.UseIdentityServerMongoDBTokenCleanup(applicationLifetime);

            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                DisplayName = "Google",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,

                ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com",
                ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo"
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
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
        }
    }
}