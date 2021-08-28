using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Propellerhead_Andy.Dtos;
using Propellerhead_Andy.Entities;
using Propellerhead_Andy.Repositories;

namespace Propellerhead_Andy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        private readonly INoteRepository noteRepository;

        public CustomerController(ICustomerRepository customerRepository, INoteRepository noteRepository)
        {
            this.customerRepository = customerRepository;
            this.noteRepository = noteRepository;
        }

        // GET /customers
        [HttpGet]
        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync([FromQuery]string orderBy = null)
        {
            var customers = (await customerRepository.GetCustomersAsync(orderBy)).Select(customer => customer.AsDto());
            return customers;
        }

        // GET /customer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerAsync(Guid id)
        {
            var customer = await customerRepository.GetCustomerAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            var notes = (await noteRepository.GetNotesByCustomerIdAsync(id)).Select(note => note.AsDto());;

            return customer.AsDto(notes);
        }

        // GET /item/field
        [HttpGet("field")]
        public async Task<IEnumerable<CustomerDto>> GetCustomerByFieldAsync([FromQuery]Status status = Status.None, [FromQuery]string name = null, [FromQuery]string contactNumber = null)
        {
            var customers = (await customerRepository.GetCustomerByFieldAsync(status, name, contactNumber)).Select(customer => customer.AsDto());

            return customers;
        }

        // POST /item
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomerAsync(CreateCustomerDto customerDto)
        {
            Customer customer = new()
            {
                Id = Guid.NewGuid(),
                Status = customerDto.Status,
                CreatedDate = DateTimeOffset.UtcNow,
                Name = customerDto.Name,
                ContactNumber = customerDto.ContactNumber
            };

            await customerRepository.CreateCustomerAsync(customer);

            return CreatedAtAction(nameof(GetCustomerAsync), new { id = customer.Id }, customer.AsDto());

        }

        // PUT /item/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomerAsync(Guid id, UpdateCustomerDto customerDto)
        {
            var existingCustomer = await customerRepository.GetCustomerAsync(id);

            if (existingCustomer is null)
            {
                return NotFound();
            }

            Customer updatedCustomer = existingCustomer with
            {
                Status = customerDto.Status,
                Name = customerDto.Name,
                ContactNumber = customerDto.ContactNumber
            };

            await customerRepository.UpdateCustomerAsync(updatedCustomer);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomerAsync(Guid id)
        {
            var existingCustomer = await customerRepository.GetCustomerAsync(id);

            if (existingCustomer is null)
            {
                return NotFound();
            }

            await customerRepository.DeleteCustomerAsync(id);

            return NoContent();
        }
    }
}