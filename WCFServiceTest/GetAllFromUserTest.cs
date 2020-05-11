using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLayer;
using ProductService.ControlLayer;

namespace WCFServiceTest
{
    [TestClass]
    public class GetAllFromUserTest
    {
        [TestMethod]
        public void TestMethod1 ( )
        {
            //Arrange
            BookingController bc = new BookingController();
            List<Booking> books = new List<Booking>();
            IEnumerable<Booking> tempBooks;

            //Act
            tempBooks = bc.GetAllFromUser("SirLol");
            foreach (Booking book in tempBooks) {
                books.Add(book);
            }

            //Assert
            Assert.IsTrue(books.Count != 0);
        }
    }
}
