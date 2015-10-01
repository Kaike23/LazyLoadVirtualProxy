using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPPatterns.Chap7.ProxyPattern.Repository
{
    using ASPPatterns.Chap7.ProxyPattern.Model;
    using System.Data.SqlClient;

    public class CustomerRepository : ICustomerRepository
    {
        private static readonly string SQL_CONNECTION = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\GitHub\LazyLoadVirtualProxy\WebApp\App_Data\TestDB.mdf;Initial Catalog=TestDB;Integrated Security=True;MultipleActiveResultSets=True";
        private IOrderRepository _orderRepository;
        public CustomerRepository(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Customer FindBy(Guid id)
        {
            Customer customer = new CustomerProxy();
            // Code to connect to the database and retrieve a customer…
            var query = "SELECT Name FROM Customer WHERE Id = @Id";
            var conn = new SqlConnection(SQL_CONNECTION);
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Id", id);
            conn.Open();
            try
            {
                var reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    reader.Read();
                    customer.Id = id;
                    customer.Name = reader.GetString(0);
                }
            }
            finally
            {
                conn.Close();
            }

            ((CustomerProxy)customer).OrderRepository = _orderRepository;
            return customer;
        }
        public IEnumerable<Customer> FindAll()
        {
            var customers = new List<Customer>();
            // Code to connect to the database and retrieve a customer…
            var query = "SELECT * FROM Customer";
            var conn = new SqlConnection(SQL_CONNECTION);
            var command = new SqlCommand(query, conn);
            conn.Open();
            try
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var customer = new Customer() { Id = reader.GetGuid(0), Name = reader.GetString(1) };
                        customers.Add(customer);
                    }
                }
            }
            finally
            {
                conn.Close();
            }

            return customers;
        }
    }
}
