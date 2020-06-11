using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using System.Data.SqlClient;
using System.Data;

namespace ProductService.DataAccessLayer {

    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    class DBBooking : IBOOKING<Booking>
    {

        private string _connectionString;
        /// <summary>
        /// the constructor for booking to the database
        /// </summary>
        public DBBooking()
        {
            _connectionString = DB.DbConnectionString;
        }

        /// <summary>
        /// the method to retrieve data form the database about the bookings for a Escape-room and on that date
        /// </summary>
        /// <param name="EscID"> the escape-room id </param>
        /// <param name="Bdate"> the date of the chosen day to the booking </param>
        /// <returns> returns a list whit all bookings for the escape-room on that date</returns>
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
        /// <summary>
        /// the method to create a booking in the database whit a transaction 
        /// </summary>
        /// <param name="book"> getting a booking from above there holds all the info about the time,date, user name, escape room id, how many people attending </param>
        public int Create(Booking book)
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
                            if (GetCheck(book.er.escapeRoomID,book.bookingTime,book.date)== false) {
                                cmdInsertBook.Parameters.AddWithValue("EscapeRoomID", book.er.escapeRoomID);
                                cmdInsertBook.Parameters.AddWithValue("BookingTime", book.bookingTime);
                                cmdInsertBook.Parameters.AddWithValue("BDate", book.date);
                                cmdInsertBook.Parameters.AddWithValue("AmountOfPeople", book.amountOfPeople);
                                cmdInsertBook.Parameters.AddWithValue("UserName", book.cus.username);
                                cmdInsertBook.Parameters.AddWithValue("EmployeeID", book.emp.employeeID);
                                cmdInsertBook.ExecuteNonQuery();
                                tran.Commit();
                                return 1;
                            }
                            else {
                                tran.Rollback();
                                return 0;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        Console.WriteLine(e);
                        Console.ReadLine();
                        return 0;
                    }
                }
            }
        }
        /// <summary>
        /// the method to delete a booking in the database
        /// </summary>
        /// <param name="book">Getting a booking from above there holds all the info about the time, date, user name, escape room id  </param>
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
        /// <summary>
        /// Getting a Booking for a user on the date and escape-room id in the database
        /// </summary>
        /// <param name="EscID">escape-room id</param>
        /// <param name="username">users user-name </param>
        /// <param name="Bdate">the date there has bin chosen</param>
        /// <returns> the complete info about the booking there has bin asked for </returns>
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
        /// <summary>
        /// Getting all the booking from the database 
        /// </summary>
        /// <returns>a list whit all bookings in the database </returns>
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
        /// <summary>
        /// the method  to retrive all booking for one user from the database
        /// </summary>
        /// <param name="username"> users user-name</param>
        /// <returns> a list whit all booking for a user </returns>
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
        /// <summary>
        /// the method to update a booking to the database
        /// </summary>
        /// <param name="BOOK">Getting a booking from above there holds all the info about the time, date, user name, escape room id, how many people and booking id</param>
        public void Update ( Booking BOOK )
        {

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (IDbTransaction tran = connection.BeginTransaction())
                    try
                    {
                        using (SqlCommand cmdUpdateBook = connection.CreateCommand())
                        {

                            cmdUpdateBook.CommandText = "UPDATE Booking SET BookingTime = @BookingTime, BDate = @BDate, AmountOfPeople = @AmountOfPeople, EscapeRoomID = @EscapeRoomID, EmployeeID = @EmployeeID, UserName = @UserName  WHERE BookingID = @BookingID";
                            cmdUpdateBook.Transaction = tran as SqlTransaction;
                            cmdUpdateBook.Parameters.AddWithValue("BookingTime", BOOK.bookingTime);
                            cmdUpdateBook.Parameters.AddWithValue("Bdate", BOOK.date);
                            cmdUpdateBook.Parameters.AddWithValue("AmountOfPeople", BOOK.amountOfPeople);
                            cmdUpdateBook.Parameters.AddWithValue("EscapeRoomID", BOOK.er.escapeRoomID);
                            cmdUpdateBook.Parameters.AddWithValue("EmployeeID", BOOK.emp.employeeID);
                            cmdUpdateBook.Parameters.AddWithValue("UserName", BOOK.cus.username);
                            cmdUpdateBook.Parameters.AddWithValue("BookingID", BOOK.Id);
                            cmdUpdateBook.ExecuteNonQuery();
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
        /// <summary>
        /// the method to get all booking on a escape-room for the database
        /// </summary>
        /// <param name="EscId">escape-room id</param>
        /// <returns> a list off booking on that escape-room </returns>
        public IEnumerable<Booking> GetAllOneRoom(int EscId) {
            List<Booking> books = new List<Booking>();
            Booking tempBook;
            DBCustomer dbcus = new DBCustomer();
            DBEscapeRoom dber = new DBEscapeRoom();
            DBEmployee dbemp = new DBEmployee();

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand cmdGetBook = connection.CreateCommand()) {
                    cmdGetBook.CommandText = "SELECT Booking.* FROM Booking WHERE EscapeRoomID = @EscapeRoomID";
                    cmdGetBook.Parameters.AddWithValue("@EscapeRoomID", EscId);
                    SqlDataReader reader = cmdGetBook.ExecuteReader();

                    while (reader.Read()) {
                        tempBook = new Booking() {
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
        public bool GetCheck(int EscID, TimeSpan BookingTime, DateTime Bdate) {
            Booking book = new Booking();
            DBCustomer dbcus = new DBCustomer();
            DBEscapeRoom dber = new DBEscapeRoom();
            DBEmployee dbemp = new DBEmployee();
            bool found = false;

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand cmdGetBook = connection.CreateCommand()) {
                    cmdGetBook.CommandText = "SELECT Booking.* FROM Booking WHERE BookingTime=@BookingTime AND EscapeRoomID =@EscapeRoomID AND BDate =@BDate";
                    cmdGetBook.Parameters.AddWithValue("@BookingTime", BookingTime);
                    cmdGetBook.Parameters.AddWithValue("@EscapeRoomID", EscID);
                    cmdGetBook.Parameters.AddWithValue("@BDate", Bdate);
                    SqlDataReader reader = cmdGetBook.ExecuteReader();
                    if (reader.Read()) {

                        book.amountOfPeople = reader.GetInt32(reader.GetOrdinal("AmountOfPeople"));
                        book.bookingTime = reader.GetTimeSpan(reader.GetOrdinal("BookingTime"));
                        book.date = reader.GetDateTime(reader.GetOrdinal("BDate"));
                        book.cus = dbcus.Get(reader.GetString(reader.GetOrdinal("UserName")));
                        book.emp = dbemp.Get(reader.GetInt32(reader.GetOrdinal("EmployeeID")));
                        book.er = dber.GetForOwner(reader.GetInt32(reader.GetOrdinal("EscapeRoomID")));
                        book.Id = reader.GetInt32(reader.GetOrdinal("BookingID"));
                        found = true;
                    }

                }
            }
            return found;
        }
    }
}
