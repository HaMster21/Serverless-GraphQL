using HotChocolate;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using ServerlessGraphQL.Schema;

[assembly: FunctionsStartup(typeof(ServerlessGraphQL.Startup))]

namespace ServerlessGraphQL
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddGraphQL(serviceProvider =>
            {
                return SchemaBuilder.New()
                                    .AddServices(serviceProvider)
                                    .AddQueryType<QueryType>()
                                    .Create();
            });
        }
    }
}
