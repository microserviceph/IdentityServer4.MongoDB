using IdentityServer4.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer4.MongoDB.Models
{
    public class Client
    {
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;
        public int AccessTokenLifetime { get; set; } = 3600;
        public int AccessTokenType { get; set; } = (int)0;
        public bool AllowAccessTokensViaBrowser { get; set; }
        public List<string> AllowedCorsOrigins { get; set; }
        public List<string> AllowedGrantTypes { get; set; }
        public List<string> AllowedScopes { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool AllowRememberConsent { get; set; } = true;
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public int AuthorizationCodeLifetime { get; set; } = 300;
        public List<ClientClaim> Claims { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public List<ClientSecret> ClientSecrets { get; set; }
        public string ClientUri { get; set; }
        public bool Enabled { get; set; } = true;

        // AccessTokenType.Jwt;
        public bool EnableLocalLogin { get; set; } = true;

        public ObjectId Id { get; set; }
        public List<string> IdentityProviderRestrictions { get; set; }
        public int IdentityTokenLifetime { get; set; } = 300;
        public bool IncludeJwtId { get; set; }
        public string LogoUri { get; set; }
        public bool LogoutSessionRequired { get; set; } = true;
        public string LogoutUri { get; set; }
        public List<string> PostLogoutRedirectUris { get; set; }
        public bool PrefixClientClaims { get; set; } = true;
        public string ProtocolType { get; set; } = ProtocolTypes.OpenIdConnect;
        public List<string> RedirectUris { get; set; }
        public int RefreshTokenExpiration { get; set; } = (int)TokenExpiration.Absolute;
        public int RefreshTokenUsage { get; set; } = (int)TokenUsage.OneTimeOnly;
        public bool RequireClientSecret { get; set; } = true;
        public bool RequireConsent { get; set; } = true;
        public bool RequirePkce { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
    }
}