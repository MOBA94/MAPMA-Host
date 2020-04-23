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
            ModelLayer.Customer customer = new Customer() {
                customerNo = cus.customerNo,
                firstName = cus.firstName,
                lastName = cus.lastName,
                mail = cus.mail,
                password = cus.password,
                phone = cus.phone,
                username = cus.username
            };
            Employee employee = new Employee() {
                address = em.address,
                cityName = em.cityName,
                employeeID = em.employeeID,
                firstName = em.firstName,
                lastName = em.lastName,
                mail = em.mail,
                phone = em.phone,
                region = em.region,
                zipcode = em.zipcode
            };
            EscapeRoom escapeRoom = new EscapeRoom() {
                cleanTime = er.cleanTime,
                description = er.description,
                escapeRoomID = er.escapeRoomID,
                maxClearTime = er.maxClearTime,
                name = er.name,
                price = er.price,
                rating = er.rating
            };
            Booking hostBook;
            Booking book = new Booking() {
                amountOfPeople = 22,
                bookingTime = DateTime.Now.TimeOfDay,
                cus = customer,
                date = DateTime.Now.AddDays(7.0).Date,
                emp = employee,
                er = escapeRoom
            };


            //Act
            bs.Create(book.emp.employeeID, book.cus.username, book.er.escapeRoomID, book.bookingTime, book.amountOfPeople, book.date);
            hostBook = bs.Get(book.er.escapeRoomID, book.cus.username, book.date);

            //Assert
            Assert.AreEqual(book.date, hostBook.date);
            Assert.AreEqual(book.emp.employeeID, hostBook.emp.employeeID);
            Assert.AreEqual(book.cus.username, hostBook.cus.username);

            bs.Delete(book.emp.employeeID, book.cus.username, book.er.escapeRoomID, book.bookingTime, book.amountOfPeople, book.date);
        }
    }
}
