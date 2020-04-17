using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using System.Data.SqlClient;

namespace ProductService.DataAccessLayer
{
    class DBBooking : IBOOKING<Booking>
    {

        private string _connectionString;

        public DBBooking() {
            _connectionString = DB.DbConnectionString;
        }

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
        public void Delete(Booking book)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand cmdDeleteBook = connection.CreateCommand()) {
                    cmdDeleteBook.CommandText = "DELETE FROM Booking WHERE UserName AND EscapeRoomID AND BDate VALUES (@UserName, @ EscapeRoomID, @BDate";
                    cmdDeleteBook.Parameters.AddWithValue("UserName", book.cus.username);
                    cmdDeleteBook.Parameters.AddWithValue("EscapeRoomID", book.er.escapeRoomID);
                    cmdDeleteBook.Parameters.AddWithValue("BDate", book.date);
                    cmdDeleteBook.ExecuteNonQuery();
                }
            }
        }

        public Booking Get(int EmpID, string username, DateTime Bdate) {
            Booking book = new Booking();
            DBCustomer dbcus = new DBCustomer();
            DBEscapeRoom dber = new DBEscapeRoom();
            DBEmployee dbemp = new DBEmployee();

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand cmdGetBook = connection.CreateCommand()) {
                    cmdGetBook.CommandText = "SELECT Booking.* FROM Booking WHERE UserName =@UserName AND EscapeRoomID =@EscapeRoomID AND BDate =@BDate";
                    cmdGetBook.Parameters.AddWithValue("UserName", username);
                    cmdGetBook.Parameters.AddWithValue("EscapeRoomID", EmpID);
                    cmdGetBook.Parameters.AddWithValue("BDate", Bdate);
                    SqlDataReader reader = cmdGetBook.ExecuteReader();
                    if (reader.Read()) {

                        book.amountOfPeople = reader.GetInt32(reader.GetOrdinal("AmountOfPeople"));
                        book.bookingTime = reader.GetDateTime(reader.GetOrdinal("Booking Time"));
                        book.date = reader.GetDateTime(reader.GetOrdinal("BDate"));
                        book.cus = dbcus.Get(username);
                        book.emp = dbemp.Get(reader.GetInt32(reader.GetOrdinal("EmployeeID")));
                        book.er = dber.GetForOwner(EmpID);
                    }

                }
            }
            return book;
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
