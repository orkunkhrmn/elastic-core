﻿using elastic_core.Dtos;
using elastic_core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace elastic_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var res = await _customerService.GetAllCustomers();
            return Ok(res);
        }

        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            var res = await _customerService.GetCustomerById(id);
            return Ok(res);
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerDto customer)
        {
            var res = await _customerService.AddCustomer(customer);
            return Ok(res);
        }
    }
}