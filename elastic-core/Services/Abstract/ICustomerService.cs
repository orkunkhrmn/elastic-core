﻿using elastic_core.Dtos;

namespace elastic_core.Services.Abstract
{
    public interface ICustomerService
    {
        Task<dynamic?> GetAllCustomers();

        Task<string> AddCustomer(CustomerDto customer);

        Task<dynamic?> GetCustomerById(string id);

        bool Delete(string id);

        public Task<dynamic> Update(Guid id, CustomerDto customer);
    }
}