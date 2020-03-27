using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using System.Data.SqlClient;

namespace ProductService.DataAccessLayer {
    class DBCustomer : ICUSTOMER<Customer> {

        private string _connectionString;

        public DBCustomer() {
            _connectionString = DB.DbConnectionString;
        }

        public void Create(Customer entity) {
            throw new NotImplementedException();
        }

        public void Delete(string username) {
            throw new NotImplementedException();
        }

        public Customer Get(string username) {
            Customer cus = new Customer();
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT FirstName, LastName, Mail, Phone, CustomerNo, UserName From Customer WHERE UserName=@UserName";
                    cmd.Parameters.AddWithValue("UserName", username);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) {
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

        public IEnumerable<Customer> GetAll() {
            throw new NotImplementedException();
        }

        public void Update(Customer entity) {
            throw new NotImplementedException();
        }
    }
}
