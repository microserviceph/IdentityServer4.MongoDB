﻿using System;
using MongoDB.Bson;
using static IdentityServer4.IdentityServerConstants;

namespace  IdentityServer4.MongoDB.Models
{
    public abstract class Secret
    {
        public ObjectId Id { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; } = SecretTypes.SharedSecret;
    }
}