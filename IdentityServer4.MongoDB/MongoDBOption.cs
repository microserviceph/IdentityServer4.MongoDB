namespace IdentityServer4.MongoDB
{
    public class ConfigurationDBOption : MongoDBOptionBase
    {
        public Option ApiResource { get; set; } = new Option
        {
            CollectionName = "ApiResource",
        };

        public Option Client { get; set; } = new Option
        {
            CollectionName = "Client",
        };

        public Option IdentityResource { get; set; } = new Option
        {
            CollectionName = "IdentityResource",
        };
    }

    public abstract class MongoDBOptionBase
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }

    public class OperationMongoDBOption : MongoDBOptionBase
    {
        public Option PersistedGrant { get; set; } = new Option
        {
            CollectionName = "PersistedGrant",
        };
        public bool EnableTokenCleanup { get; set; } = true;
        public int TokenCleanupInterval { get; set; } = 3600;
        // public int TokenCleanupBatchSize { get; set; } = 100;
        
    }

    public class Option
    {
        public string CollectionName { get; set; }

        public bool ManageIndicies { get; set; } = true;
    }
}