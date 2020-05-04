using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductService.ControlLayer;

namespace WCFServiceTest
{
    [TestClass]
    public class SaltAndHashTest
    {
        [TestMethod]
        public void TestMethod1 ( )
        {
            //Arrange
            CustomerController cc = new CustomerController();
            string password = "gruppe2ErSeje";




                //Act
            string hashedPassword = cc.PasswordHashAndSalt(password);
            


            //Assert
            Assert.AreNotEqual(hashedPassword, password);
            Assert.IsTrue(ProductService.ControlLayer.CustomerController.VerifyHashedPassword(hashedPassword, password));
           
        }
    }
}
