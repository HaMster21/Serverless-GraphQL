using System.Threading.Tasks;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace ServerlessGraphQL
{
    public class GraphQL
    {
        private readonly IQueryExecutor Executor;

        public GraphQL(IQueryExecutor executor)
            => Executor = executor ?? throw new System.ArgumentNullException(nameof(executor));


        [FunctionName("graphql")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GraphQL-Endpoint triggered");

            return new OkResult();
        }
    }
}
