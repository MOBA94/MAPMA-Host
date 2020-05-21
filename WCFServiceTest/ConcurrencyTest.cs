using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLayer;
using ProductService.ControlLayer;
using System.Threading;

namespace WCFServiceTest
{
    [TestClass]
    public class ConcurrencyTest
    {
        private static AutoResetEvent autoEvent;

        [TestMethod]
        public void TestMethod1()
        {
            autoEvent = new AutoResetEvent(false);
            ThreadStart threadDelegate1 = new ThreadStart(firstBooking);
            ThreadStart threadDelegate2 = new ThreadStart(secondBooking);
            Thread thread1 = new Thread(threadDelegate1);
            Thread thread2 = new Thread(threadDelegate2);
            thread1.Start();
            thread2.Start();
            thread1.Join();

        }

        private void firstBooking() {
            //Arrange
            CustomerController cusctr = new CustomerController();
            EmployeeController empctr = new EmployeeController();
            EscapeRoomController escctr = new EscapeRoomController();
            BookingController bookctr = new BookingController();
            Customer cus = cusctr.Get("SirLol");
            Employee emp = empctr.Get(1);
            EscapeRoom er = escctr.GetForOwner(3);
            Booking checkBook;
            Booking book = new Booking
            {
                amountOfPeople = 5,
                bookingTime = new TimeSpan(16, 00, 0000),
                cus = cus,
                date = DateTime.Now.AddDays(14).Date,
                emp = emp,
                er = er,
            };

            //Act
            autoEvent.WaitOne();
            bookctr.Create(book.emp.employeeID, book.cus.username, book.er.escapeRoomID, book.bookingTime, book.amountOfPeople, book.date);
            checkBook = bookctr.Get(er.escapeRoomID, cus.username, book.date);

            //Assert
            Assert.AreNotEqual(book.amountOfPeople, checkBook.amountOfPeople);
            autoEvent.Set();

        }

        private void secondBooking() {
            //Arrange
            CustomerController cusctr2 = new CustomerController();
            EmployeeController empctr2 = new EmployeeController();
            EscapeRoomController escctr2 = new EscapeRoomController();
            BookingController bookctr2 = new BookingController();
            Customer cus2 = cusctr2.Get("Anorak");
            Employee emp2 = empctr2.Get(1);
            EscapeRoom er2 = escctr2.GetForOwner(3);
            Booking checkBook2;
            Booking book2 = new Booking {
                  amountOfPeople =  2,
                  bookingTime = new TimeSpan(16, 00, 0000),
                  cus = cus2,
                  date = DateTime.Now.AddDays(14).Date,
                  emp = emp2,
                  er = er2,   
                 

            };

            //Act
            bookctr2.Create(book2.emp.employeeID, book2.cus.username, book2.er.escapeRoomID, book2.bookingTime, book2.amountOfPeople, book2.date);
                 checkBook2 = bookctr2.Get(er2.escapeRoomID, cus2.username, book2.date);

            //Assert
            Assert.AreEqual(book2.amountOfPeople, checkBook2.amountOfPeople);
            autoEvent.Set();
            autoEvent.WaitOne();
            bookctr2.Delete(book2.emp.employeeID, book2.cus.username, book2.er.escapeRoomID, book2.bookingTime, book2.amountOfPeople, book2.date);


        }
    }
}
