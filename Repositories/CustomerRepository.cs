using System.Data;
using System.Data.SqlClient;
using Dapper;
using DapperApiDemo.Models;

namespace DapperApiDemo.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Customer>(
                "GetAllCustomers", commandType: CommandType.StoredProcedure);
        }
        public async Task<Customer> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();

            var result = await connection.QueryFirstOrDefaultAsync<Customer>(
                "GetCustomerById",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<int> AddAsync(Customer customer)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(
                "AddCustomer", new { customer.Name, customer.Email }, commandType: CommandType.StoredProcedure);
        }

        // public async Task<int> UpdateAsync(Customer customer)
        // {
        //     using var connection = CreateConnection();
        //     return await connection.ExecuteAsync(
        //         "UpdateCustomer",
        //         new { customer.Id, customer.Name, customer.Email },
        //         commandType: CommandType.StoredProcedure);
        // }


        public async Task<Customer> UpdateAsync(Customer customer)
        {
            using var connection = CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<Customer>(
                "UpdateCustomer",
                new { customer.Id, customer.Name, customer.Email },
                commandType: CommandType.StoredProcedure);
            return result;
        }


        public async Task<int> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(
                "DeleteCustomer", new { Id = id }, commandType: CommandType.StoredProcedure);
        }
    }
}
