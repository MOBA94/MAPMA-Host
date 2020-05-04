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
                salt = ProductService.ControlLayer.CustomerController.CreateSalt();
            string hashedPassword = ProductService.ControlLayer.CustomerController.PasswordHashAndSalt(password, salt);
            


            //Assert
            Assert.AreNotEqual(hashedPassword, password);
            Assert.IsTrue(ProductService.ControlLayer.CustomerController.VerifyHashedPassword(hashedPassword, password, salt));
           
        }
    }
}
