using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.ControlLayer;
namespace ProductService {
   [ServiceBehavior(Namespace = "http://localhost:8734/Design_Time_Addresses/Booking")]

    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    public class BookingServices : IBookingServices {

        /// <summary>
        /// the method to receive the input to the attributes from either MVC or Desktop client
        /// </summary>
        /// <param name="EmpID"> Employees id</param>
        /// <param name="username">customers user-name</param>
        /// <param name="ER_ID"> Escape-romms id</param>
        /// <param name="bookTime"> the time off the day for the booking </param>
        /// <param name="AOP"> amount of peaplo atending the booking</param>
        /// <param name="Bdate"> the date for the booking</param>
        /// <returns></returns>
        public int Create(int EmpID, string username, int ER_ID, TimeSpan bookTime, int AOP, DateTime Bdate) {

            BookingController BCon = new BookingController();

            return BCon.Create(EmpID, username, ER_ID, bookTime, AOP, Bdate);
        }

        /// <summary>
        /// the method to delete a booking from desktop Client
        /// </summary>
        /// <param name="EscID"> Employees id</param>
        /// <param name="username">customers user-name</param>
        /// <param name="ER_ID">Escape-romms id</param>
        /// <param name="bookTime">the time off the day for the booking </param>
        /// <param name="AOP">amount of peaplo atending the booking</param>
        /// <param name="Bdate">the date for the booking</param>
        public void Delete(int EscID, string username, int ER_ID, TimeSpan bookTime, int AOP, DateTime Bdate) {
            BookingController BCon = new BookingController();
            BCon.Delete(EscID, username, ER_ID, bookTime, AOP, Bdate);
        }
        /// <summary>
        /// the method to delete a booking from web client
        /// </summary>
        /// <param name="username">customers user-name</param>
        /// <param name="ER_ID">Escape-rooms id</param>
        /// <param name="bookTime">the time off the day for the booking </param>
        /// <param name="Bdate">the date for the booking</param>
        public void Deleteweb(string username, int ER_ID, TimeSpan bookTime , DateTime Bdate) {
            BookingController BCon = new BookingController();
            BCon.DeleteWeb(username, ER_ID, bookTime, Bdate);
        }
        /// <summary>
        /// the method to get a booking for a customer whit the date and escape-room id 
        /// </summary>
        /// <param name="EmpID">escape-room id</param>
        /// <param name="username">customers user-name</param>
        /// <param name="Bdate">the date for the booking</param>
        /// <returns> return the booking whit the right attributes  </returns>
        public Booking Get(int EmpID, string username, DateTime Bdate) {
            BookingController BCon = new BookingController();

            return BCon.Get(EmpID, username, Bdate);
        }
        /// <summary>
        /// the method to retrieve all the booking there is down in the database
        /// </summary>
        /// <returns>a list whit all bookings</returns>
        public IEnumerable<Booking> GetAll ( )
        {
            BookingController bookCon = new BookingController();

            return bookCon.GetAll();
        }

        /// <summary>
        /// the method to update a booking whit the new info
        /// </summary>
        /// <param name="EmpID"> Employee id</param>
        /// <param name="username"> customers user-name</param>
        /// <param name="ER_ID">escape-rooms id </param>
        /// <param name="bookTime"> the time of the day</param>
        /// <param name="AOP"> amount of People</param>
        /// <param name="Bdate"> the date for a day</param>
        /// <param name="bookId">booking id</param>
        public void Update(int EmpID, string username, int ER_ID, TimeSpan bookTime, int AOP, DateTime Bdate, int bookId) {
            BookingController bookCon = new BookingController();
            bookCon.Update(EmpID, username, ER_ID, bookTime, AOP, Bdate, bookId);
        }
        /// <summary>
        /// the method to get all booking for a customer whit that user-name
        /// </summary>
        /// <param name="username">customers user-name</param>
        /// <returns>a list whit all bookings for a customer</returns>
        public IEnumerable<Booking> GetAllFromUser(string username) {
            BookingController bookCon = new BookingController();

            return bookCon.GetAllFromUser(username);
        }
        /// <summary>
        /// the method to retrieve all the booking for a escape room whit that id
        /// </summary>
        /// <param name="EscId">escape-rooms id</param>
        /// <returns>a list whit all booking for that escape-room </returns>
        public IEnumerable<Booking> GetAllOneRoom(int EscId) {
            BookingController bookCon = new BookingController();

            return bookCon.GetAllOneRoom(EscId);
        }
    }
}
