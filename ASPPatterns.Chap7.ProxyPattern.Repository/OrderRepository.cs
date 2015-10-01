using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPPatterns.Chap7.ProxyPattern.Repository
{
    using ASPPatterns.Chap7.ProxyPattern.Model;
    using System.Data.SqlClient;

    public class OrderRepository : IOrderRepository
    {
        private static readonly string SQL_CONNECTION = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\GitHub\LazyLoadVirtualProxy\WebApp\App_Data\TestDB.mdf;Initial Catalog=TestDB;Integrated Security=True;MultipleActiveResultSets=True";

        public IEnumerable<Order> FindAllBy(Guid customerId)
        {
            List<Order> customerOrders = new List<Order>();
            // Code to connect to the database and populate the collection
            // of customers’ orders...
            var query = "SELECT Id, OrderDate FROM [Order] WHERE CustomerId = @CustomerId";
            var conn = new SqlConnection(SQL_CONNECTION);
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@CustomerId", customerId);
            conn.Open();
            try
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        customerOrders.Add(new Order() { Id = reader.GetGuid(0), OrderDate = reader.GetDateTime(1) });
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return customerOrders;
        }
    }
}
