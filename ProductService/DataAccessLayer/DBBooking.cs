using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using System.Data.SqlClient;

namespace ProductService.DataAccessLayer
{
    class DBBooking : ICRUD<T>
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
                    cmdInsertBook.Parameters.AddWithValue("EscapeRoomID", book.er.ID);
                    cmdInsertBook.Parameters.AddWithValue("BookingTime", book.bookingTime);
                    cmdInsertBook.Parameters.AddWithValue("BDate", book.date);
                    cmdInsertBook.Parameters.AddWithValue("AmountOfPeople", book.amountOfPeople);
                    cmdInsertBook.Parameters.AddWithValue("UserName", book.cus.UserName);
                    cmdInsertBook.Parameters.AddWithValue("EmployeeID", book.emp.ID);
                    cmdInsertBook.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
