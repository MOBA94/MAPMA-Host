using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLayer;
using ProductService;

namespace WCFServiceTest
{
    [TestClass]
    public class CreateNewBookingTest
    {
        [TestMethod]
        public void CreateBooking ( ){

            IBookingServices bs = new BookingServices();
            ICustomerServices cs = new CustomerServices();
            IEscapeRoom_Services es = new EscapeRoom_Services();
            IEmplyeeServices ems = new EmplyeeServices();

            ModelLayer.Customer cus = cs.Get("Anorak");
            ModelLayer.Employee em = ems.Get(1);
            ModelLayer.EscapeRoom er = es.GetForOwner(1);
            Booking hostBook;
            Booking book = new Booking() {
                amountOfPeople = 22,
                bookingTime = new TimeSpan(16,00,0000),
                cus = cus,
                date = DateTime.Now.AddDays(7.0).Date,
                emp = em,
                er = er
            };


            //Act
            bs.Create(book.emp.employeeID, book.cus.username, book.er.escapeRoomID, book.bookingTime, book.amountOfPeople, book.date);
            hostBook = bs.Get(book.er.escapeRoomID, book.cus.username, book.date);

            //Assert
            Assert.AreEqual(book.date.Date, hostBook.date.Date);
            Assert.AreEqual(book.emp.employeeID, hostBook.emp.employeeID);
            Assert.AreEqual(book.cus.username, hostBook.cus.username);

            bs.Delete(book.emp.employeeID, book.cus.username, book.er.escapeRoomID, book.bookingTime, book.amountOfPeople, book.date);

            Assert.IsNull(bs.Get(book.er.escapeRoomID, book.cus.username, book.date).cus);
        }
    }
}
