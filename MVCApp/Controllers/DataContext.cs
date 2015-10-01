using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApp.Controllers
{
    using MVCApp.Models;

    public class DataContext
    {
        private static readonly string SQL_CONNECTION = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\GitHub\LazyLoadInitialization\LazyInitialization\App_Data\LazyInitializationContext-20150929102005.mdf;Initial Catalog=LazyInitializationContext-20150929102005;Integrated Security=True;MultipleActiveResultSets=True";

        private List<Product> products;

        public DataContext()
        {
        }

        public List<Product> Products
        {
            get
            {
                //if (products == null) products = GetProducts();
                //return products;
                return GetProducts();
            }
        }

        public List<Product> GetProducts()
        {
            var query = "SELECT * FROM Products";
            var connection = new SqlConnection(SQL_CONNECTION);
            var command = new SqlCommand(query, connection);
            connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    products = new List<Product>();
                    while (reader.Read())
                    {
                        var product = new Product() { Id = reader.GetGuid(0), Name = reader.GetString(1), Description = reader.GetString(2) };
                        products.Add(product);
                    }
                }
            }
            finally
            {
                connection.Close();
            }
            return products;
        }

        internal void Remove(Product product)
        {
            var query = "DELETE FROM Products WHERE Id = @Id";
            var connection = new SqlConnection(SQL_CONNECTION);
            connection.Open();
            using (var trans = connection.BeginTransaction())
            {
                try
                {
                    var command = new SqlCommand(query, connection, trans);
                    command.Parameters.AddWithValue("@Id", product.Id);
                    var rowCount = command.ExecuteNonQuery();
                    if (rowCount == 0)
                    {
                        throw new Exception(string.Format("Error: Product '{0}' was not deleted.", product.Name));
                    }
                    trans.Commit();
                    //products.Remove(product);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Something went wrong. " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        internal void Create(Product product)
        {
            var query = "INSERT INTO Products VALUES(@Id, @Name, @Description)";
            var connection = new SqlConnection(SQL_CONNECTION);
            connection.Open();
            using (var trans = connection.BeginTransaction())
            {
                try
                {
                    var command = new SqlCommand(query, connection, trans);
                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    var rowCount = command.ExecuteNonQuery();
                    if (rowCount == 0)
                    {
                        throw new Exception(string.Format("Error: Product '{0}' was not created.", product.Name));
                    }
                    trans.Commit();
                    //products.Add(product);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Something went wrong. " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        internal void SaveChanges(Product product)
        {
            var query = "UPDATE Products SET Name = @Name, Description = @Description WHERE Id = @Id";
            var connection = new SqlConnection(SQL_CONNECTION);
            connection.Open();
            using (var trans = connection.BeginTransaction())
            {
                try
                {
                    var command = new SqlCommand(query, connection, trans);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Id", product.Id);
                    var rowCount = command.ExecuteNonQuery();
                    if (rowCount == 0)
                    {
                        throw new Exception(string.Format("Error: Product '{0}' was not modified.", product.Name));
                    }
                    trans.Commit();
                    //var index = products.FindIndex(x => x.Id == product.Id);
                    //products[index] = product;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Something went wrong. " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
