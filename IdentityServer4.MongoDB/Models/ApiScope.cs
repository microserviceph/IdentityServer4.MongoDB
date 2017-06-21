using MongoDB.Bson;
using System.Collections.Generic;

namespace IdentityServer4.MongoDB.Models
{
    public class ApiScope
    {
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public bool Emphasize { get; set; }
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<string> UserClaims { get; set; }
    }
}