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

    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    {
        private string _connectionString;
        /// <summary>
        /// the constructor for employee to the database
        /// </summary>
        public DBEmployee() {
            _connectionString = DB.DbConnectionString;
        }

        /// <summary>
        /// not implemented yet
        /// </summary>
        /// <param name="entity"></param>
        public void Create ( Employee entity )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// not implemented yet
        /// </summary>
        /// <param name="id"></param>
        public void Delete ( int id )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// the method to get a employee for the database
        /// </summary>
        /// <param name="id">the employees id</param>
        /// <returns>a employee whit all the info about the person</returns>
        public Employee Get ( int id )
        {
            Employee employee = new Employee();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM Employee LEFT JOIN City ON Employee.Zipcode = City.Zipcode WHERE EmployeeId = @EmployeeId";
                    cmd.Parameters.AddWithValue("EmployeeId", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) {
                        employee.address = reader.GetString(reader.GetOrdinal("EAddress"));
                        employee.cityName = reader.GetString(reader.GetOrdinal("CityName"));
                        employee.employeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID"));
                        employee.firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        employee.lastName = reader.GetString(reader.GetOrdinal("LastName"));
                        employee.phone = reader.GetString(reader.GetOrdinal("Phone"));
                        employee.region = reader.GetString(reader.GetOrdinal("Region"));
                        employee.zipcode = reader.GetInt32(reader.GetOrdinal("Zipcode"));
                        employee.mail = reader.GetString(reader.GetOrdinal("Mail"));
                    }
                }
            }

                    return employee;
        }
        /// <summary>
        /// a method to get all the employee for the database whit all the info about the employees
        /// </summary>
        /// <returns>a list off employees</returns>
        public IEnumerable<Employee> GetAll ( )
        {
            List<Employee> Employees = new List<Employee>();
            Employee tempEmp;
            ;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand cmdReadAllEmps = connection.CreateCommand()) {

                    cmdReadAllEmps.CommandText = "SELECT Employee.*, City.* From Employee LEFT JOIN City ON Employee.Zipcode = City.Zipcode";
                    SqlDataReader reader = cmdReadAllEmps.ExecuteReader();

                    while (reader.Read()) {
                        tempEmp = new Employee();
                        tempEmp.address = reader.GetString(reader.GetOrdinal("EAddress"));
                        tempEmp.cityName = reader.GetString(reader.GetOrdinal("CityName"));
                        tempEmp.employeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID"));
                        tempEmp.firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        tempEmp.lastName = reader.GetString(reader.GetOrdinal("LastName"));
                        tempEmp.phone = reader.GetString(reader.GetOrdinal("Phone"));
                        tempEmp.region = reader.GetString(reader.GetOrdinal("Region"));
                        tempEmp.zipcode = reader.GetInt32(reader.GetOrdinal("Zipcode"));
                        tempEmp.mail = reader.GetString(reader.GetOrdinal("Mail"));

                        Employees.Add(tempEmp);

                    }
                }
            }
            return Employees;
        }
        /// <summary>
        /// not implemented yet
        /// </summary>
        /// <param name="entity"></param>
        public void Update ( Employee entity )
        {
            throw new NotImplementedException();
        }
    }
}
