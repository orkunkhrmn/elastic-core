using elastic_core.Models;
using Elasticsearch.Net;
using Nest;

namespace elastic_core.Extensions
{
    public static class ElasticSearhExtensions
    {
        public static void AddElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("customer");

            var client = new ElasticClient(connectionSettings);

            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, "customer");
        }

        private async static void CreateIndex(IElasticClient client, string indexName)
        {
            Customer customer = new Customer { Id = Guid.NewGuid(), FirstName = "Orkun", LastName = "Kahraman" };

            var indexResponse = await client.IndexAsync(customer, idx => idx.Index(indexName).OpType(OpType.Index));
        }
    }
}
