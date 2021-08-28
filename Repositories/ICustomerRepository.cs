using System;
using Propellerhead_Andy.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Propellerhead_Andy.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerAsync(Guid id);
        Task<IEnumerable<Customer>> GetCustomerByFieldAsync(Status status = Status.None, string name = null, string contactNumber = null);
        Task<IEnumerable<Customer>> GetCustomersAsync(string orderBy = null);
        Task CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid id);
    }
}