﻿using CustomerWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly CustomerDbContext _context;

        public CustomerController(CustomerDbContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Customer>> GetCustomers() {
            var customers =  _context.Customers;
            return Ok(customers);
        }

        [HttpGet("{customerId:int}")]
        [Authorize]
        public async Task<ActionResult<Customer>> GetCustomerById(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            return customer;
        }

        [HttpPost]
        [Authorize(Roles ="Administrator")]
        public async Task<ActionResult> Create(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult> Update(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{customerId:int}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Customer>> Delete(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
