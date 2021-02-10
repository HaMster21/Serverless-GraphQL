using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServerlessGraphQL
{
    internal class GraphQLRequest
    {
        public string Query { get; set; }
        public Dictionary<string, object> Variables { get; set; }
    }

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

            var graphql = JsonConvert.DeserializeObject<GraphQLRequest>(await req.ReadAsStringAsync());

            var result = await Executor.ExecuteAsync(reqBuilder =>
            {
                reqBuilder.SetQuery(graphql.Query);
                reqBuilder.SetVariableValues(graphql.Variables);
            });

            if (result.Errors.Any())
            {
                throw new AggregateException("GraphQL operation failed", result.Errors.Select(error => error.Exception));
            }

            return new OkObjectResult(result);
        }
    }
}
