using IdentityServer4.Models;
using System.Linq;
using System.Security.Claims;

namespace IdentityServer4.MongoDB
{
    public static class Mapper
    {
        public static Models.Client ToEntity(this Client client)
        {
            return new Models.Client
            {
                AbsoluteRefreshTokenLifetime = client.AbsoluteRefreshTokenLifetime,
                AccessTokenLifetime = client.AbsoluteRefreshTokenLifetime,
                AccessTokenType = (int)client.AccessTokenType,
                AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
                AllowedCorsOrigins = client.AllowedCorsOrigins.ToList(),
                AllowedGrantTypes = client.AllowedGrantTypes.ToList(),
                AllowedScopes = client.AllowedScopes.ToList(),
                AllowOfflineAccess = client.AllowOfflineAccess,
                AllowPlainTextPkce = client.AllowPlainTextPkce,
                AllowRememberConsent = client.AllowRememberConsent,
                AlwaysIncludeUserClaimsInIdToken = client.AlwaysIncludeUserClaimsInIdToken,
                AlwaysSendClientClaims = client.AlwaysSendClientClaims,
                AuthorizationCodeLifetime = client.AuthorizationCodeLifetime,
                Claims = client.Claims
                            .Select(c => new Models.ClientClaim(c.Type, c.Value))
                            .ToList(),
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                ClientSecrets = client.ClientSecrets
                            .Select(c => new Models.ClientSecret
                            {
                                Description = c.Description,
                                Expiration = c.Expiration,
                                Type = c.Type,
                                Value = c.Value
                            })
                            .ToList(),
                ClientUri = client.ClientUri,
                Enabled = client.Enabled,
                EnableLocalLogin = client.EnableLocalLogin,
                IdentityProviderRestrictions = client.IdentityProviderRestrictions.ToList(),
                IdentityTokenLifetime = client.IdentityTokenLifetime,
                IncludeJwtId = client.IncludeJwtId,
                LogoUri = client.LogoUri,
                LogoutSessionRequired = client.LogoutSessionRequired,
                LogoutUri = client.LogoutUri,
                PostLogoutRedirectUris = client.PostLogoutRedirectUris.ToList(),
                ProtocolType = client.ProtocolType,
                PrefixClientClaims = client.PrefixClientClaims,
                UpdateAccessTokenClaimsOnRefresh = client.UpdateAccessTokenClaimsOnRefresh,
                SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime,
                RequirePkce = client.RequirePkce,
                RequireConsent = client.RequirePkce,
                RedirectUris = client.RedirectUris.ToList(),
                RefreshTokenExpiration = (int)client.RefreshTokenExpiration,
                RefreshTokenUsage = (int)client.RefreshTokenUsage,
                RequireClientSecret = client.RequireClientSecret
            };
        }

        public static Client ToModel(this Models.Client client)
        {
            return new Client
            {
                AbsoluteRefreshTokenLifetime = client.AbsoluteRefreshTokenLifetime,
                AccessTokenLifetime = client.AbsoluteRefreshTokenLifetime,
                AccessTokenType = (AccessTokenType)client.AccessTokenType,
                AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
                AllowedCorsOrigins = client.AllowedCorsOrigins,
                AllowedGrantTypes = client.AllowedGrantTypes,
                AllowedScopes = client.AllowedScopes,
                AllowOfflineAccess = client.AllowOfflineAccess,
                AllowPlainTextPkce = client.AllowPlainTextPkce,
                AllowRememberConsent = client.AllowRememberConsent,
                AlwaysIncludeUserClaimsInIdToken = client.AlwaysIncludeUserClaimsInIdToken,
                AlwaysSendClientClaims = client.AlwaysSendClientClaims,
                AuthorizationCodeLifetime = client.AuthorizationCodeLifetime,
                Claims = client.Claims
                            .Select(c => new Claim(c.Type, c.Value))
                            .ToList(),
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                ClientSecrets = client.ClientSecrets
                            .Select(c => new Secret
                            {
                                Description = c.Description,
                                Expiration = c.Expiration,
                                Type = c.Type,
                                Value = c.Value
                            })
                            .ToList(),
                ClientUri = client.ClientUri,
                Enabled = client.Enabled,
                EnableLocalLogin = client.EnableLocalLogin,
                IdentityProviderRestrictions = client.IdentityProviderRestrictions,
                IdentityTokenLifetime = client.IdentityTokenLifetime,
                IncludeJwtId = client.IncludeJwtId,
                LogoUri = client.LogoUri,
                LogoutSessionRequired = client.LogoutSessionRequired,
                LogoutUri = client.LogoutUri,
                PostLogoutRedirectUris = client.PostLogoutRedirectUris,
                ProtocolType = client.ProtocolType,
                PrefixClientClaims = client.PrefixClientClaims,
                UpdateAccessTokenClaimsOnRefresh = client.UpdateAccessTokenClaimsOnRefresh,
                SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime,
                RequirePkce = client.RequirePkce,
                RequireConsent = client.RequirePkce,
                RedirectUris = client.RedirectUris,
                RefreshTokenExpiration = (TokenExpiration)client.RefreshTokenExpiration,
                RefreshTokenUsage = (TokenUsage)client.RefreshTokenUsage,
                RequireClientSecret = client.RequireClientSecret
            };
        }

        public static Models.ApiResource ToEntity(this ApiResource model)
        {
            return new Models.ApiResource(model.Name, model.DisplayName, model.UserClaims)
            {
                Secrets = model?.ApiSecrets
                               .Select(c => new Models.ApiSecret
                               {
                                   Type = c.Type,
                                   Value = c.Value,
                                   Description = c.Description,
                                   Expiration = c.Expiration
                               })
                               .ToList(),
                Enabled = model.Enabled,
                Description = model.Description,
                Scopes = model?.Scopes
                           .Select(c => new Models.ApiScope
                           {
                               Description = c.Description,
                               DisplayName = c.DisplayName,
                               Emphasize = c.Emphasize,
                               Name = c.Name,
                               UserClaims = c.UserClaims.ToList(),
                               Required = c.Required,
                               ShowInDiscoveryDocument = c.ShowInDiscoveryDocument
                           })
                           .ToList()
            };
        }

        public static Models.IdentityResource ToEntity(this IdentityResource model)
        {
            return new Models.IdentityResource(model.Name, model.DisplayName, model.UserClaims)
            {
                Enabled = model.Enabled,
                Description = model.Description,
                Required = model.Required,
                Emphasize = model.Emphasize,
                ShowInDiscoveryDocument = model.ShowInDiscoveryDocument
            };
        }

        public static ApiResource ToModel(this Models.ApiResource model)
        {
            return new ApiResource(model.Name, model.DisplayName, model.UserClaims)
            {
                ApiSecrets = model?.Secrets
                                .Select(c => new Secret(c.Value, c.Description, c.Expiration)
                                {
                                    Type = c.Type
                                })
                                .ToList(),
                Enabled = model.Enabled,
                Description = model.Description,
                Scopes = model?.Scopes
                            .Select(c => new Scope
                            {
                                Description = c.Description,
                                DisplayName = c.DisplayName,
                                Emphasize = c.Emphasize,
                                Name = c.Name,
                                UserClaims = c.UserClaims,
                                Required = c.Required,
                                ShowInDiscoveryDocument = c.ShowInDiscoveryDocument
                            })
                            .ToList()
            };
        }

        public static IdentityResource ToModel(this Models.IdentityResource model)
        {
            return new IdentityResource(model.Name, model.DisplayName, model.UserClaims)
            {
                Enabled = model.Enabled,
                Description = model.Description,
                Required = model.Required,
                Emphasize = model.Emphasize,
                ShowInDiscoveryDocument = model.ShowInDiscoveryDocument
            };
        }

    }
}