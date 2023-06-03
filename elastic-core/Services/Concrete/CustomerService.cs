using elastic_core.Dtos;
using elastic_core.Models;
using elastic_core.Services.Abstract;
using Elasticsearch.Net;
using Nest;

namespace elastic_core.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly IElasticClient _client;

        public CustomerService(IElasticClient client)
        {
            _client = client;
        }

        public async Task<string> AddCustomer(CustomerDto customerDto)
        {
            Customer customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
            };

            var indexResponse = await _client.IndexAsync(customer, idx =>
                idx.Index("customer").OpType(OpType.Index)
            );

            return indexResponse.IsValid ? Convert.ToString(indexResponse.Id) : "Unable to add the item";
        }

        public bool Delete(string id)
        {
            var searchResponse = _client.Delete<Customer>(id);
            return searchResponse.IsValid;
        }

        public async Task<dynamic?> GetAllCustomers()
        {
            var searchResponse = await _client.SearchAsync<dynamic>(s => s.Index("customer").Query(q => q.MatchAll()));
            return searchResponse.IsValid ? searchResponse.Documents.ToList() : default;
        }

        public async Task<dynamic?> GetCustomerById(string id)
        {
            var searchResponse = await _client.SearchAsync<Customer>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Id)
                        .Query(id)
                    )
                )
            );

            return searchResponse.IsValid ? searchResponse.Documents.ToList().FirstOrDefault() : default;
        }

        public bool Update(Guid id, CustomerDto customer)
        {
            var updatedCustomer = new Customer { Id = id };

            var response = _client.Update(DocumentPath<Customer>
                    .Id(id),
                    u => u
                        .Index("customer")
                        .DocAsUpsert(true)
                        .Doc(updatedCustomer));

            return response.IsValid;
        }
    }
}