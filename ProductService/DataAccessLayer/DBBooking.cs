using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using System.Data.SqlClient;
using System.Data;

namespace ProductService.DataAccessLayer {
    class DBBooking : IBOOKING<Booking>
    {

        private string _connectionString;

        public DBBooking()
        {
            _connectionString = DB.DbConnectionString;
        }

        public List<Booking> CheckBooking(int EscID, DateTime Bdate)
        {
            Booking TempBook;
            List<Booking> book = new List<Booking>();
            DBEscapeRoom dber = new DBEscapeRoom();
            DBEmployee dbemp = new DBEmployee();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmdGetBook = connection.CreateCommand())
                {
                    cmdGetBook.CommandText = "SELECT Booking.* FROM Booking WHERE EscapeRoomID =@EscapeRoomID AND BDate =@BDate";
                    //skal være et lop og en excutebatch tror jeg. skal også retunere et list af bookings
                    cmdGetBook.Parameters.AddWithValue("@EscapeRoomID", EscID);
                    cmdGetBook.Parameters.AddWithValue("@BDate", Bdate);
                    SqlDataReader reader = cmdGetBook.ExecuteReader();
                    while (reader.Read())
                    {
                        TempBook = new Booking();
                        TempBook.amountOfPeople = reader.GetInt32(reader.GetOrdinal("AmountOfPeople"));
                        TempBook.bookingTime = reader.GetTimeSpan(reader.GetOrdinal("BookingTime"));
                        TempBook.date = reader.GetDateTime(reader.GetOrdinal("BDate"));
                        TempBook.emp = dbemp.Get(reader.GetInt32(reader.GetOrdinal("EmployeeID")));
                        TempBook.er = dber.GetForOwner(EscID);
                        book.Add(TempBook);
                    }
                }
            }
            return book;
        }

        public void Create(Booking book)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (IDbTransaction tran = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmdInsertBook = connection.CreateCommand())
                        {
                            cmdInsertBook.CommandText = "INSERT INTO Booking(EscapeRoomID, BookingTime," +
                                " BDate, AmountOfPeople, UserName, EmployeeID)" +
                                " VALUES(@EscapeRoomID, @BookingTime, @BDate, @AmountOfPeople, @UserName, @EmployeeID)";
                            cmdInsertBook.Transaction = tran as SqlTransaction;
                            cmdInsertBook.Parameters.AddWithValue("EscapeRoomID", book.er.escapeRoomID);
                            cmdInsertBook.Parameters.AddWithValue("BookingTime", book.bookingTime);
                            cmdInsertBook.Parameters.AddWithValue("BDate", book.date);
                            cmdInsertBook.Parameters.AddWithValue("AmountOfPeople", book.amountOfPeople);
                            cmdInsertBook.Parameters.AddWithValue("UserName", book.cus.username);
                            cmdInsertBook.Parameters.AddWithValue("EmployeeID", book.emp.employeeID);
                            cmdInsertBook.ExecuteNonQuery();
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

        public void Delete(Booking book)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmdDeleteBook = connection.CreateCommand())
                {
                    cmdDeleteBook.CommandText = "DELETE FROM Booking WHERE UserName =@UserName AND EscapeRoomID =@EscapeRoomID AND BDate =@BDate AND BookingTime =@BookingTime";
                    cmdDeleteBook.Parameters.AddWithValue("UserName", book.cus.username);
                    cmdDeleteBook.Parameters.AddWithValue("EscapeRoomID", book.er.escapeRoomID);
                    cmdDeleteBook.Parameters.AddWithValue("BDate", book.date);
                    cmdDeleteBook.Parameters.AddWithValue("BookingTime", book.bookingTime);

                    cmdDeleteBook.ExecuteNonQuery();
                }
            }
        }

        public Booking Get(int EscID, string username, DateTime Bdate)
        {
            Booking book = new Booking();
            DBCustomer dbcus = new DBCustomer();
            DBEscapeRoom dber = new DBEscapeRoom();
            DBEmployee dbemp = new DBEmployee();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmdGetBook = connection.CreateCommand())
                {
                    cmdGetBook.CommandText = "SELECT Booking.* FROM Booking WHERE UserName =@UserName AND EscapeRoomID =@EscapeRoomID AND BDate =@BDate";
                    cmdGetBook.Parameters.AddWithValue("@UserName", username);
                    cmdGetBook.Parameters.AddWithValue("@EscapeRoomID", EscID);
                    cmdGetBook.Parameters.AddWithValue("@BDate", Bdate);
                    SqlDataReader reader = cmdGetBook.ExecuteReader();
                    if (reader.Read())
                    {

                        book.amountOfPeople = reader.GetInt32(reader.GetOrdinal("AmountOfPeople"));
                        book.bookingTime = reader.GetTimeSpan(reader.GetOrdinal("BookingTime"));
                        book.date = reader.GetDateTime(reader.GetOrdinal("BDate"));
                        book.cus = dbcus.Get(reader.GetString(reader.GetOrdinal("UserName")));
                        book.emp = dbemp.Get(reader.GetInt32(reader.GetOrdinal("EmployeeID")));
                        book.er = dber.GetForOwner(reader.GetInt32(reader.GetOrdinal("EscapeRoomID")));
                        book.Id = reader.GetInt32(reader.GetOrdinal("BookingID"));
                    }

                }
            }
            return book;
        }

        public IEnumerable<Booking> GetAll ( )
        {
            List<Booking> books = new List<Booking>();
            Booking tempBook;
            DBCustomer dbcus = new DBCustomer();
            DBEscapeRoom dber = new DBEscapeRoom();
            DBEmployee dbemp = new DBEmployee();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmdGetBook = connection.CreateCommand())
                {
                    cmdGetBook.CommandText = "SELECT Booking.* FROM Booking";
                    SqlDataReader reader = cmdGetBook.ExecuteReader();

                    while (reader.Read())
                    {
                        tempBook = new Booking()
                        {
                            amountOfPeople = reader.GetInt32(reader.GetOrdinal("AmountOfPeople")),
                            bookingTime = reader.GetTimeSpan(reader.GetOrdinal("BookingTime")),
                            cus = dbcus.Get(reader.GetString(reader.GetOrdinal("UserName"))),
                            date = reader.GetDateTime(reader.GetOrdinal("BDate")),
                            emp = dbemp.Get(reader.GetInt32(reader.GetOrdinal("EmployeeID"))),
                            er = dber.GetForOwner(reader.GetInt32(reader.GetOrdinal("EscapeRoomID"))),
                            Id = reader.GetInt32(reader.GetOrdinal("BookingID"))
                        };

                        books.Add(tempBook);

                    }
                }
            }
            return books;
        }

        public IEnumerable<Booking> GetAllFromUser ( string username )
        {
            List<Booking> books = new List<Booking>();
            Booking tempBook;
            DBCustomer dbcus = new DBCustomer();
            DBEscapeRoom dber = new DBEscapeRoom();
            DBEmployee dbemp = new DBEmployee();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmdGetBook = connection.CreateCommand())
                {
                    cmdGetBook.CommandText = "SELECT Booking.* FROM Booking WHERE UserName = @UserName";
                    cmdGetBook.Parameters.AddWithValue("@UserName",  username);
                    SqlDataReader reader = cmdGetBook.ExecuteReader();

                    while (reader.Read())
                    {
                        tempBook = new Booking()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("BookingID")),
                            amountOfPeople = reader.GetInt32(reader.GetOrdinal("AmountOfPeople")),
                            bookingTime = reader.GetTimeSpan(reader.GetOrdinal("BookingTime")),
                            cus = dbcus.Get(reader.GetString(reader.GetOrdinal("UserName"))),
                            date = reader.GetDateTime(reader.GetOrdinal("BDate")),
                            emp = dbemp.Get(reader.GetInt32(reader.GetOrdinal("EmployeeID"))),
                            er = dber.GetForOwner(reader.GetInt32(reader.GetOrdinal("EscapeRoomID")))
                        };

                        books.Add(tempBook);

                    }
                }
            }
            return books;
        }

        public void Update ( Booking BOOK )
        {

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand cmdUpdateBook = connection.CreateCommand()) {

                    cmdUpdateBook.CommandText = "UPDATE Booking SET BookingTime = @BookingTime, BDate = @BDate, AmountOfPeople = @AmountOfPeople, EscapeRoomID = @EscapeRoomID, EmployeeID = @EmployeeID, UserName = @UserName  WHERE BookingID = @BookingID";
                    cmdUpdateBook.Parameters.AddWithValue("BookingTime", BOOK.bookingTime);
                    cmdUpdateBook.Parameters.AddWithValue("Bdate", BOOK.date);
                    cmdUpdateBook.Parameters.AddWithValue("AmountOfPeople", BOOK.amountOfPeople);
                    cmdUpdateBook.Parameters.AddWithValue("EscapeRoomID", BOOK.er.escapeRoomID);
                    cmdUpdateBook.Parameters.AddWithValue("EmployeeID", BOOK.emp.employeeID);
                    cmdUpdateBook.Parameters.AddWithValue("UserName", BOOK.cus.username);
                    cmdUpdateBook.Parameters.AddWithValue("BookingID", BOOK.Id);
                    cmdUpdateBook.ExecuteNonQuery();
                }
            }
        }
    }
}
