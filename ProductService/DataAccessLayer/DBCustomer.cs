using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace ProductService.DataAccessLayer
{
    public class DBCustomer : ICUSTOMER<Customer>
    {

        private string _connectionString;

        public DBCustomer ( )
        {
            _connectionString = DB.DbConnectionString;
        }

        public void Register(Customer cus) {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (IDbTransaction tran = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmdRegister = connection.CreateCommand()) {

                    cmdRegister.CommandText = "INSERT INTO Customer(FirstName, LastName, Mail, Phone, UserName, Salt, CPassword)"  +
                         "VALUES(@FirstName, @LastName, @Mail, @Phone, @UserName, @Salt, @CPassword)";
                    cmdRegister.Transaction = tran as SqlTransaction;
                    cmdRegister.Parameters.AddWithValue("FirstName",cus.firstName);
                    cmdRegister.Parameters.AddWithValue("LastName",cus.lastName);
                    cmdRegister.Parameters.AddWithValue("Mail",cus.mail);
                    cmdRegister.Parameters.AddWithValue("Phone",cus.phone);
                    cmdRegister.Parameters.AddWithValue("UserName",cus.username);
                    cmdRegister.Parameters.AddWithValue("Salt",cus.salt);
                    cmdRegister.Parameters.AddWithValue("CPassword",cus.password);
                    cmdRegister.ExecuteNonQuery();
                    tran.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        Console.WriteLine(e);
                        Console.ReadLine();
                    }
                }
            }
        }

        public void Delete ( string username )
        {
            throw new NotImplementedException();
        }

        public Customer Get ( string username )
        {
            Customer cus = new Customer();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT FirstName, LastName, Mail, Phone, CustomerNo, UserName, Salt From Customer WHERE UserName=@UserName";
                    cmd.Parameters.AddWithValue("UserName", username);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        cus.firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        cus.lastName = reader.GetString(reader.GetOrdinal("LastName"));
                        cus.mail = reader.GetString(reader.GetOrdinal("Mail"));
                        cus.phone = reader.GetString(reader.GetOrdinal("Phone"));
                        cus.customerNo = reader.GetInt32(reader.GetOrdinal("CustomerNo"));
                        cus.username = reader.GetString(reader.GetOrdinal("UserName"));
                        cus.salt = reader.GetString(reader.GetOrdinal("Salt"));
                    }
                }
            }
            return cus;
        }

        public Customer Login ( string username, string password )
        {
            Customer cus = new Customer();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT FirstName, LastName, Mail, Phone, CustomerNo, UserName From Customer WHERE UserName=@UserName AND CPassword = @CPassword";
                    cmd.Parameters.AddWithValue("UserName", username);
                    cmd.Parameters.AddWithValue("CPassword", password);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        cus.firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        cus.lastName = reader.GetString(reader.GetOrdinal("LastName"));
                        cus.mail = reader.GetString(reader.GetOrdinal("Mail"));
                        cus.phone = reader.GetString(reader.GetOrdinal("Phone"));
                        cus.customerNo = reader.GetInt32(reader.GetOrdinal("CustomerNo"));
                        cus.username = reader.GetString(reader.GetOrdinal("UserName"));
                    }
                }
            }
            return cus;
        }

        public IEnumerable<Customer> GetAll ( )
        {
            throw new NotImplementedException();
        }

        public void Update ( Customer entity )
        {
            throw new NotImplementedException();
        }

    }
}

