using DapperApiDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperApiDemo.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task<int> AddAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer);
        Task<int> DeleteAsync(int id);
    }
}