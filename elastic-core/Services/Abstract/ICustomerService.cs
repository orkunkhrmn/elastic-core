using elastic_core.Dtos;

namespace elastic_core.Services.Abstract
{
    public interface ICustomerService
    {
        Task<dynamic?> GetAllCustomers();
        Task<string> AddCustomer(CustomerDto customer);
    }
}
