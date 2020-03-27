using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using System.Data.SqlClient;
//tjekker om jeg kan pull det her
namespace ProductService.DataAccessLayer
{
    class DBBooking : IBOOKING<Booking>
    {

        private string _connectionString;

        public void Create(Booking book)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmdInsertBook = connection.CreateCommand())
                {
                    cmdInsertBook.CommandText = "INSERT INTO Booking(EscapeRoomID, BookingTime, BDate, AmountOfPeople, UserName, EmployeeID) VALUES(@EscapeRoomID, @BookingTime, @BDate, @AmountOfPeople, @UserName, @EmployeeID)";
                    cmdInsertBook.Parameters.AddWithValue("EscapeRoomID", book.er.escapeRoomID);
                    cmdInsertBook.Parameters.AddWithValue("BookingTime", book.bookingTime);
                    cmdInsertBook.Parameters.AddWithValue("BDate", book.date);
                    cmdInsertBook.Parameters.AddWithValue("AmountOfPeople", book.amountOfPeople);
                    cmdInsertBook.Parameters.AddWithValue("UserName", book.cus.username);
                    cmdInsertBook.Parameters.AddWithValue("EmployeeID", book.emp.employeeID);
                    cmdInsertBook.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Booking Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Booking entity)
        {
            throw new NotImplementedException();
        }
    }
}
