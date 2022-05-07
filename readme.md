#### IdentityServer 5.x-6.x Data Persistence for MongoDB

###### How to use

1. Install Package
```shell
Install-Package Miracle.IdentityServer.Storage.MongoDB
```
2. Add the following code to the Program.cs file in the root of the project
```csharp
var dboptions = new MiracleMongoOptions();
// add some option to ignor identity info such as the _id property(but it doesn't seem to work 😂)
dboptions.AppendConventionRegistry("IdentityServerMongoConventions", new()
{
    Conventions = new()
    {
        new IgnoreExtraElementsConvention(true)
    },
    Filter = _ => true
});

var db = await builder.Services.AddMongoDbContext<BaseDbContext>(clientSettings: new()
{
    ServerAddresses = new()
    {
		// Add the connection ip and port to the MongoDB server
        new("192.168.2.10", 27017),
    },
	// Add the auth database name
    AuthDatabase = "admin",
	// Add the database name for the IdentityServer data
    DatabaseName = "miracleidentityserver",
	// your mongodb database username
    UserName = "xxxxxx",
	// your mongodb database password
    Password = "&xxxxxx",
}, dboptions: dboptions);

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
})
    .AddMongoRepository(db)
    .AddDeveloperSigningCredential()
    .AddIdentityClients()
    .AddIdentityResources()    
    .AddIdentityPersistedGrants()
    .AddPolicyService();

//.AddCustomTokenRequestValidator<CustomTokenRequestValidator>()
//// not recommended for production - you need to store your key material somewhere secure

// Create initial resources from Identity Default Config.cs
SeedDatabase.Seed(builder.Services);
```
