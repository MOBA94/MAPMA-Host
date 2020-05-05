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
            List<Employee> Employees = new List<Employee>();
            Employee tempEmp;
            String tempCheck;
            ;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand cmdReadAllEmps = connection.CreateCommand()) {

                    cmdReadAllEmps.CommandText = "SELECT Employee.*, City.* From Employee LEFT JOIN City ON Employee.Zipcode = City.Zipcode";
                    SqlDataReader reader = cmdReadAllEmps.ExecuteReader();

                    while (reader.Read()) {
                        tempEmp = new Employee();
                        tempEmp.address = reader.GetString(reader.GetOrdinal("Eaddress"));
                        tempEmp.cityName = reader.GetString(reader.GetOrdinal("Cityname"));
                        tempEmp.employeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID"));
                        tempEmp.firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        tempEmp.lastName = reader.GetString(reader.GetOrdinal("LastName"));
                        tempEmp.phone = reader.GetString(reader.GetOrdinal("Phone"));
                        tempEmp.region = reader.GetString(reader.GetOrdinal("Region"));
                        tempEmp.zipcode = reader.GetInt32(reader.GetOrdinal("Zipcode"));
                  
                    }
                }
            }
            return Employees;
        }

        public void Update ( Employee entity )
        {
            throw new NotImplementedException();
        }
    }
}
