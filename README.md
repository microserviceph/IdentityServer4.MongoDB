# IdentityServer4.MongoDB

Identity store using MongoDB

[![m1chael-dg MyGet Build Status](https://www.myget.org/BuildSource/Badge/m1chael-dg?identifier=86defe33-6d5c-48a3-a72b-12b8ffb828bc)](https://www.myget.org/)


### IIdentityServerBuilder extensions:
```csharp
services.AddIdentityServer()
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
```

### IApplicationBuilder extensions:
```csharp
services.UseIdentityServerMongoDBTokenCleanup(applicationLifetime);
```
