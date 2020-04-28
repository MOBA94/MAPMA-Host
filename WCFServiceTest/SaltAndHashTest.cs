using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WCFServiceTest
{
    [TestClass]
    public class SaltAndHashTest
    {
        [TestMethod]
        public void TestMethod1 ( )
        {
            //Arrange
            string password = "gruppe2ErSeje";
            string salt;




                //Act
                salt = ProductService.DataAccessLayer.DBCustomer.CreateSalt();
            string hashedPassword = ProductService.DataAccessLayer.DBCustomer.PasswordHashAndSalt(password, salt);
            


            //Assert
            Assert.AreNotEqual(hashedPassword, password);
            Assert.IsTrue(ProductService.DataAccessLayer.DBCustomer.VerifyHashedPassword(hashedPassword, password, salt));
           
        }
    }
}
