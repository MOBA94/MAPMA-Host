using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.DataAccessLayer;

namespace ProductService.ControlLayer {

    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    public class BookingController {
        private CustomerController CusCon;
        private EscapeRoomController ERCon;
        private EmployeeController ECon;
        private IBOOKING<Booking> dbBook;


        /// <summary>
        /// the constructor for BookingControiler
        /// </summary>
        public BookingController() {
            CusCon = new CustomerController();
            ERCon = new EscapeRoomController();
            ECon = new EmployeeController();
            dbBook = new DBBooking();
        }


        /// <summary>
        /// the method to create a booking whit all the parameters
        /// </summary>
        /// <param name="EmpID"> Employees id</param>
        /// <param name="username">Customers User name</param>
        /// <param name="ER_ID"> Escape-rooms id</param>
        /// <param name="bookTime">the time off the day</param>
        /// <param name="AOP">amount of people </param>
        /// <param name="Bdate">the date of a day</param>
        /// <returns>returns a int to tell if the booking is completede or not</returns>
        public int Create(int EmpID, string username, int ER_ID, TimeSpan bookTime, int AOP, DateTime Bdate) {
            //List<TimeSpan> checklist = ERCon.FreeTimes(ER_ID, Bdate);
            //if (checklist.Count == 0) {
            //    return 0;
            //}
            //else {
            //    if (checklist.Contains(bookTime)) {
                            Booking tempBook = new Booking {
                            emp = ECon.Get(EmpID),
                            cus = CusCon.Get(username),
                            er = ERCon.GetForOwner(ER_ID)
                        };
                        tempBook.bookingTime = bookTime;
                        tempBook.amountOfPeople = AOP;
                        tempBook.date = Bdate;
                        
                        return dbBook.Create(tempBook);
                        //return 1;
                    //}
                    //else {
                    //    return 0;
                    //} 
            //}
            
        }


        /// <summary>
        /// the method to delete a booking
        /// </summary>
        /// <param name="EmpID"> Employees id</param>
        /// <param name="username"> Customers User name</param>
        /// <param name="ER_ID"> Escape-rooms id</param>
        /// <param name="bookTime">the time off the day</param>
        /// <param name="AOP">amount of people </param>
        /// <param name="Bdate">the date of a day</param>
        public void Delete(int EmpID, string username, int ER_ID, TimeSpan bookTime, int AOP, DateTime Bdate) {
            
            Booking tempBook = new Booking {
                emp = ECon.Get(EmpID),
                cus = CusCon.Get(username),
                er = ERCon.GetForOwner(ER_ID),
                amountOfPeople = AOP,
                bookingTime = bookTime,
                date = Bdate
            };

            dbBook.Delete(tempBook);
        }

        /// <summary>
        /// the method to delete a booking for the web
        /// </summary>       
        /// <param name="username"> Customers User name</param>
        /// <param name="ER_ID"> Escape-rooms id</param>
        /// <param name="bookTime">the time off the day</param>       
        /// <param name="Bdate">the date of a day</param>
        public void DeleteWeb(string username, int ER_ID, TimeSpan bookTime, DateTime Bdate) {

            Booking tempBook = new Booking {                
                cus = CusCon.Get(username),
                er = ERCon.GetForOwner(ER_ID),                
                bookingTime = bookTime,
                date = Bdate
            };

            dbBook.Delete(tempBook);
        }

        /// <summary>
        /// the method to update a booking 
        /// </summary>
        /// <param name="EmpID"> Employees id</param>
        /// <param name="username"> Customers User name</param>
        /// <param name="ER_ID"> Escape-rooms id</param>
        /// <param name="bookTime">the time off the day</param>
        /// <param name="AOP">amount of people </param>
        /// <param name="Bdate">the date of a day</param>
        /// <param name="bookId">booking id</param>
        public void Update(int EmpID, string username, int ER_ID, TimeSpan bookTime, int AOP, DateTime Bdate, int bookId)
        {
            Booking bk = new Booking()
            {
                emp = ECon.Get(EmpID),
                amountOfPeople = AOP,
                bookingTime = bookTime,
                cus = CusCon.Get(username),
                date = Bdate,
                er = ERCon.GetForOwner(ER_ID),
                Id = bookId
            };

            dbBook.Update(bk);
        }

        /// <summary>
        /// get a specific booking whit the parameters 
        /// </summary>
        /// <param name="EscID"> Escape-rooms id</param>
        /// <param name="username">Customers User name</param>
        /// <param name="Bdate">the date of a day</param>
        /// <returns></returns>
        public Booking Get(int EscID, string username, DateTime Bdate) {
            return dbBook.Get(EscID, username, Bdate);
        }

        /// <summary>
        /// the method to get all the bookings in the system
        /// </summary>
        /// <returns>a list off all bookings</returns>
        public IEnumerable<Booking> GetAll() {
            return dbBook.GetAll();
        }
        /// <summary>
        /// the method to get all the bookings for a Customer
        /// </summary>
        /// <param name="username">Customers User name</param>
        /// <returns>a list off all bookings for a customer</returns>
        public IEnumerable<Booking> GetAllFromUser(string username) {

            return dbBook.GetAllFromUser(username);
        }
        /// <summary>
        /// get all booking for a Escape-room
        /// </summary>
        /// <param name="EscId">Escape-rooms id</param>
        /// <returns>a list of bookings for a escape-room</returns>
        public IEnumerable<Booking> GetAllOneRoom(int EscId) {
            return dbBook.GetAllOneRoom(EscId);
        }
        /// <summary>
        /// a method to check the bookings for a escape-room one the date 
        /// </summary>
        /// <param name="EscID">Escape-rooms id</param>
        /// <param name="Bdate"> the date of a day</param>
        /// <returns></returns>
        public List<Booking> CheckBooking(int EscID, DateTime Bdate) {
            return dbBook.CheckBooking(EscID, Bdate);
        }
    }
}
