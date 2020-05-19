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
        /// <summary>
        /// the constructor for Customer to the database
        /// </summary>
        public DBCustomer ( )
        {
            _connectionString = DB.DbConnectionString;
        }
        /// <summary>
        /// the method to create a new customer whit all the info on the customer to put in the database 
        /// </summary>
        /// <param name="cus"> getting a Customer from above there holds all the info about FirstName, LastName, Mail, Phone, UserName, and the hassing + salted password </param>
        public void Register(Customer cus) {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (IDbTransaction tran = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmdRegister = connection.CreateCommand()) {

                    cmdRegister.CommandText = "INSERT INTO Customer(FirstName, LastName, Mail, Phone, UserName, CPassword)"  +
                         "VALUES(@FirstName, @LastName, @Mail, @Phone, @UserName, @CPassword)";
                    cmdRegister.Transaction = tran as SqlTransaction;
                    cmdRegister.Parameters.AddWithValue("FirstName",cus.firstName);
                    cmdRegister.Parameters.AddWithValue("LastName",cus.lastName);
                    cmdRegister.Parameters.AddWithValue("Mail",cus.mail);
                    cmdRegister.Parameters.AddWithValue("Phone",cus.phone);
                    cmdRegister.Parameters.AddWithValue("UserName",cus.username);
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
        /// <summary>
        /// getting a customer on the user-name from the database
        /// </summary>
        /// <param name="username">Customers user-name</param>
        /// <returns> a customer whit all info FirstName, LastName, Mail, Phone, CustomerNo , UserName and password from the database </returns>
        public Customer Get ( string username )
        {
            Customer cus = new Customer();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT FirstName, LastName, Mail, Phone, CustomerNo, UserName, CPassword From Customer WHERE UserName=@UserName";
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
                        cus.password = reader.GetString(reader.GetOrdinal("CPassword"));
                    }
                }
            }
            return cus;
        }
      
        /// <summary>
        /// not implemented yet
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetAll ( )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// not implemented yet
        /// </summary>
        /// <param name="entity"></param>
        public void Update ( Customer entity )
        {
            throw new NotImplementedException();
        }

    }
}

