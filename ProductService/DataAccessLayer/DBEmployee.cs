using ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.DataAccessLayer
{
    class DBEmployee : IEMPLOYEE<Employee>
    {
        private string _connectionString;

        public DBEmployee() {
            _connectionString = DB.DbConnectionString;
        }

        public void Create ( Employee entity )
        {
            throw new NotImplementedException();
        }

        public void Delete ( int id )
        {
            throw new NotImplementedException();
        }

        public Employee Get ( int id )
        {
            Employee employee = new Employee();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT FirstName, EmployeeId, LastName FROM Employee WHERE EmployeeId = @EmployeeId";
                    cmd.Parameters.AddWithValue("EmployeeId", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) {
                        employee.employeeID = reader.GetInt32(reader.GetOrdinal("EmployeeId"));
                        employee.firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        employee.lastName = reader.GetString(reader.GetOrdinal("LastName"));
                    }
                }
            }

                    return employee;
        }

        public IEnumerable<Employee> GetAll ( )
        {
            throw new NotImplementedException();
        }

        public void Update ( Employee entity )
        {
            throw new NotImplementedException();
        }
    }
}
